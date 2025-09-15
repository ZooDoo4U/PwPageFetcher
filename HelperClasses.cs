using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PwPageFetcher
{
    public class PageInfo
    {
        public string FullUrl       { get; set; }
        public string PageContent   { get; set; }
    }
    
    public class PwPageModel
    {
        public Guid password                        { get; set; }
        public Guid userID                          { get; set; }
        public string userName                      { get; set; }
        public Dictionary<string, string> pageInfo  { get; set; }

        public PwPageModel( Guid userId, Guid password, string userName, Dictionary<string, string> pageInfo )
        {
            this.userID   = userId;
            this.userName = userName;
            this.password = password;
            this.pageInfo = pageInfo;
        }
    }

    public static class Utils
    {
        public static bool StrCompare( this string str1, string str2 )
        {
            return string.Compare( str1, str2, StringComparison.InvariantCultureIgnoreCase ) == 0;
        }

        public static bool StrContains( this string str, bool Ignorecase = true )
        {
            return str.Contains( str, Ignorecase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture );
        }

        public static void SaveJson<T>( T data, string fileName )
        {
            using(StreamWriter sw = new StreamWriter( fileName ))
            {
                sw.Write( JsonConvert.SerializeObject( data, Newtonsoft.Json.Formatting.Indented ) );
                sw.Flush();
            }
        }

        public static T ReadJson<T>( string fileName )
        {
            using(StreamReader sr = new StreamReader( fileName ))
            {
                string json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<T>( json );
            }
        }

        public static void Sleep( int ms = 1000 )
        {
            System.Threading.Thread.Sleep( ms );
        }

        public static string LowercaseFirstLetter( this string str )
        {
            if(string.IsNullOrEmpty( str ))
            {
                return str;
            }

            if(char.IsLower( str[0] ))
            {
                return str;
            }

            return char.ToLowerInvariant( str[0] ) + str.Substring( 1 );
        }

        public static string GetUrlWithoutParams( this string url )
        {
            Uri uri = new Uri(url);
            string[] segments = uri.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
            string path = string.Join("/", segments.Take(3));
            return $"{uri.Scheme}://{uri.Host}{(uri.IsDefaultPort ? "" : ":" + uri.Port)}/{path}";
        }
    }
}
