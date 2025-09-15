using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using PwPageFetcher;

namespace PwCdpPageScrapper
{
    public partial class frmMain : Form
    {

        public  IPlaywright     pw;
        public  IBrowser        browser;
        public  IBrowserContext context;
        public  IPage           page;
        public  Process         process;
        public  int             PwPort ;
        private string htmlClassName { get; set; }

        Regex rePage = new Regex("(about:|gum.criteo.com|ads.|onetag-sys.|eus.rubiconproject.com|eb2.3lift.com|amazon|adtrafficquality|taboola|google|marketplace|adnxs|adserv|doubleclick|adengine|chrome-error:|.smilewanted.|sdk.streamrail|js-sec.indexww.com)",RegexOptions.Compiled| RegexOptions.IgnoreCase);
        Regex reUrl = new Regex("^([0-9.]+/)*(.+):*[0-9]+(?<foo>.+$)",RegexOptions.Compiled|RegexOptions.IgnoreCase);

        public Dictionary<string,string> allFrames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private string iFrame;

        public frmMain()
        {
            InitializeComponent();
            StartUp();
        }

        private async void StartUp()
        {
            try
            {
                IPlaywright playwright = await Playwright.CreateAsync();
                browser = await playwright.Chromium.LaunchAsync( new BrowserTypeLaunchOptions
                {
                    Headless = false,
                    Args = new[] { "--remote-debugging-port=9233" }
                } );

                context = await browser.NewContextAsync();
                page = await context.NewPageAsync();
                Utils.Sleep( 2000 );
              
                await page.ReloadAsync();
                Utils.Sleep( 1500 );
            }
            catch(Exception ex)
            {
                browser = null;
                context = null;
                page = null;
            }
        }

        private void SetupBrowser()
        {
            try
            {
                
            }
            catch
            {
            }
        }

        private async void cmdLaunch_Click( object sender, EventArgs e )
        {
            try
            {
                allFrames.Clear();
                txtPageSource.Text = String.Empty;
                txtJsonCode.Text = String.Empty;     

                WebScraper scraper = new WebScraper();
                List<PageInfo> pages  = await scraper.ScrapePageAndIframesAsync(page.Url);
                foreach(PageInfo p in pages)
                {
                    AddNewPage( p.FullUrl, p.PageContent.ToString() );
                }

                foreach(string k in allFrames.Keys)
                {
                    txtPageSource.Text += $"{k}\r\n";
                }

                ////////////////////////////
                var pageSource= await SendPages();
                if(pageSource == "")
                {
                    txtJsonCode.Text = "oops!?  No page source could be found...  User && Password don't match";
                    return;
                }

                this.htmlClassName = GetModelName( allFrames.Keys.First() );
                lblHtmlModelName.Text = this.htmlClassName;
                txtJsonCode.Text = pageSource;

            }
            catch(Exception ex)
            {
                txtPageSource.Text = ex.ToString();
            }
            return;
        }

        private bool SkipUrl( string url )
        {
            return rePage.IsMatch( url );
        }

        private void AddNewPage( string pageUrl, string pageSource )
        {
            if(!allFrames.ContainsKey( pageUrl ))
            {
                allFrames.Add( pageUrl, pageSource );
            }
        }

        private void cmdExitProgram_Click( object sender, EventArgs e )
        {
            this.Close();
        }

        public async Task ProcessFrameAsync( IFrame frame, int depth )
        {
            string indent = new string(' ', depth * 2);

            try
            {
                if(!allFrames.ContainsKey( frame.Url ))
                {
                    string frameContent = await frame.ContentAsync();
                    AddNewPage( frame.Url, frameContent );
                }
                else
                {
                    Console.WriteLine( $"already added {frame.Url}" );
                }

            }
            catch(PlaywrightException ex)
            {
                Console.WriteLine( $"{indent}Error accessing frame content: {ex.Message}" );
            }

            IReadOnlyList<IFrame> childFrames = frame.ChildFrames;
            foreach(IFrame childFrame in childFrames)
            {
                await ProcessFrameAsync( childFrame, depth + 1 );
            }
        }

        private async void cmdGetPoms_Click( object sender, EventArgs e )
        {
            if(allFrames.Count() < 1)
            {
                MessageBox.Show( "Please scan page first..." );
                return;
            }

            var pageSource= await SendPages();
            if(pageSource == null)
            {
                txtJsonCode.Text = "oops!?  No page source could be found...";
                return;
            }

            this.htmlClassName = GetModelName( allFrames.Keys.First() );
            lblHtmlModelName.Text = this.htmlClassName;
            txtJsonCode.Text = pageSource;
        }


        public string GetModelName( string uriRoute )
        {
          
            Uri uri = new Uri(uriRoute);

            string cleanUri  = uri.AbsolutePath.TrimStart('/');
            cleanUri = $"{uri.Host.Trim()}{uri.AbsolutePath.Trim()}";
            
            int portIndex = cleanUri.LastIndexOf(':');
            if(portIndex != -1)
            {
                cleanUri = cleanUri.Substring( 0, portIndex );
            }

            if( reUrl.IsMatch(cleanUri))
            {
                Match m = reUrl.Match(cleanUri);
                cleanUri = m.Groups[3].Value;  
            }

            cleanUri = cleanUri.Replace( "/", "" ).Replace( ".html", "", StringComparison.OrdinalIgnoreCase );
            cleanUri = cleanUri.Trim('_');

            return cleanUri;
        }

        private async Task<String> SendPages()
        {
            string apiUrl   = ConfigurationManager.AppSettings["url"];
            string userId   = ConfigurationManager.AppSettings["userid"];
            string password = ConfigurationManager.AppSettings["password"];
            string userName = ConfigurationManager.AppSettings["userName"];

            Console.WriteLine( $"Api Url: {apiUrl}" );
            Console.WriteLine( $"userId: {userId}" );
            Console.WriteLine( $"password: {password}" );
            Console.WriteLine( $"userName: {userName}" ); 

            //String apiUrl =url;// "http://50.35.86.193:3333";
            //apiUrl = " http://localhost:5255";

            //
            // Create an instance of the PwPageModel to send
            //

            PwPageModel dataToSend = new PwPageModel(
                Guid.Parse(userId),
                Guid.Parse(password),
                userName,
                allFrames
            );

            using(HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri( apiUrl );
                    var postResponse= await client.PostAsJsonAsync<PwPageModel>("translate", dataToSend);

                    if(postResponse.IsSuccessStatusCode)
                    {
                        var fooo = await postResponse.Content.ReadFromJsonAsync<string>();
                        return fooo;
                    }
                    else
                    {
                        throw new HttpRequestException( $"Request failed with status code: {postResponse.StatusCode}" );
                    }
                }
                catch(HttpRequestException ex)
                {
                    Console.WriteLine( $"Error sending POST request: {ex.Message}" );
                }
                catch(Exception ex)
                {
                    Console.WriteLine( $"An unexpected error occurred: {ex.Message}" );
                }
            }
            return "";
        }

        private void cmdSave_Click( object sender, EventArgs e )
        {
            if(allFrames.Count() < 1)
            {
                MessageBox.Show( "No Pages found..." );
                return;
            }

            frmGetFileName dlg = new frmGetFileName();
            dlg.txtFileName.Text = $"{this.htmlClassName}.raw";
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                string xxx = dlg.txtFileName.Text;

                try
                {
                    Utils.SaveJson<Dictionary<string, string>>( allFrames, xxx );
                }
                catch(Exception ex)
                {

                }
            }
        }

        private void cmdSavePageJS_Click( object sender, EventArgs e )
        {
            string leCode = txtJsonCode.Text.Trim();
            if(string.IsNullOrEmpty( leCode ))
            {
                MessageBox.Show( "Appears no page has been processed?" );
                return;
            }

            var saveDlg  = new SaveFileDialog();
            saveDlg.CheckFileExists = false;
            saveDlg.FileName = $"{htmlClassName}.ts";

            if(saveDlg.ShowDialog() == DialogResult.OK)
            {
                string xx =  saveDlg.FileName;
                File.WriteAllText( xx, txtJsonCode.Text );
            }
        }

        public async Task<List<PageInfo>> ScrapePageAndIframesAsync( string url )
        {
            var pageInfoList = new List<PageInfo>();

            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();

            try
            {                
                await page.GotoAsync( url, new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded } );
                pageInfoList.Add( new PageInfo
                {
                    FullUrl = page.Url,
                    PageContent = await page.ContentAsync()
                } );                
            }
            catch(PlaywrightException ex)
            {
                Console.WriteLine( $"An error occurred while navigating or scraping: {ex.Message}" );
            }
            catch(Exception ex)
            {
                Console.WriteLine( $"An unexpected error occurred: {ex.Message}" );
            }
            finally
            {                
                await browser.CloseAsync();
            }

            return pageInfoList;
        }

        private async Task GetIframeContentRecursiveXX( IFrame frame, List<PageInfo> pageInfoList )
        {
            var iframeElements = await frame.QuerySelectorAllAsync("iframe");
            var count = iframeElements.Count;

            if(count == 0)
            {
                return;
            }
            
            foreach(var iframeElement in iframeElements)
            {
                IFrame nestedFrame = null;
                try
                {
                    nestedFrame = await iframeElement.ContentFrameAsync();
                }
                catch(Exception ex)
                {                
                    continue;
                }

                if(nestedFrame != null)
                {                    
                    pageInfoList.Add( new PageInfo
                    {
                        FullUrl = nestedFrame.Url,
                        PageContent = await nestedFrame.ContentAsync()
                    } );
                }
                else
                {
                    Console.WriteLine( $"  Warning: ContentFrameAsync returned null for an iframe in {frame.Url}. It might not have loaded or be accessible." );
                }
            }
        }
    }

    public class WebScraper
    {
        public async Task<List<PageInfo>> ScrapePageAndIframesAsync( string url )
        {
            var pageInfoList = new List<PageInfo>();

            
            using var playwright = await Playwright.CreateAsync();

            
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });
            
            var page = await browser.NewPageAsync();
            try
            {
                await page.GotoAsync( url, new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded } );
                pageInfoList.Add( new PageInfo
                {
                    FullUrl = page.Url,
                    PageContent = await page.ContentAsync()
                } );

                await GetIframeContentRecursive( page.MainFrame, pageInfoList );
            }
            catch(PlaywrightException ex)
            {
                Console.WriteLine( $"An error occurred while navigating or scraping: {ex.Message}" );
            }
            catch(Exception ex)
            {
                Console.WriteLine( $"An unexpected error occurred: {ex.Message}" );
            }
            finally
            {
                await browser.CloseAsync();
            }

            return pageInfoList;
        }

        private async Task GetIframeContentRecursive( IFrame frame, List<PageInfo> pageInfoList )
        {
            var iframeElements = await frame.QuerySelectorAllAsync("iframe");
            var count = iframeElements.Count;

            if(count == 0)
            {
                return;
            }
            
            foreach(var iframeElement in iframeElements)
            {
                IFrame nestedFrame = null;
                try
                {                    
                    nestedFrame = await iframeElement.ContentFrameAsync();
                    if(nestedFrame.Url.IndexOf( "https://google", StringComparison.OrdinalIgnoreCase ) >= 0)
                    {
                        continue;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine( $"Warning: Could not get content frame for an iframe in {frame.Url}. Error: {ex.Message}" );
                    continue;
                }

                if(nestedFrame != null)
                {                 
                    if(nestedFrame.Url.StrCompare( "about:blank" ))
                    {
                        continue;
                    }

                    pageInfoList.Add( new PageInfo
                    {
                        FullUrl = nestedFrame.Url,
                        PageContent = await nestedFrame.ContentAsync()
                    } );
                 
                    await GetIframeContentRecursive( nestedFrame, pageInfoList );
                }
                else
                {
                    Console.WriteLine( $"  Warning: ContentFrameAsync returned null for an iframe in {frame.Url}. It might not have loaded or be accessible." );
                }
            }
        }
    }
}