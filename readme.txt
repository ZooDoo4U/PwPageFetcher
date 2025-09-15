PWPageFetcher


Description:    A utility program to generate a POM (Page Object Model) for a given web site's page.

Usage:          Start the program, upon starting you should see the main dialog appear. 
                Along with the dialog there should be a Chrome browser window appear 
                for you to navigate to the pages you with to get a POM for.  One you are happy
                with the page, in the main form press "Scrap Browser".  Should take a few second and
                the text box on the form should appears with the results, should be the page sorce 
                for the POM.  You may now use the "Save JS Code" to save away the POM for the page. 

Overview:       Currently the POM generaete will get any and everything on the page, better to have and
                not need, then need and not have.  As of now it follows a pattern should be:

                [get|set]_controlType_identifier_name

                get_text_firstName 

                <or> 

                [frame_Name]_[get|set]_phoneNumber -- for elements in an iframe

                For nested iframe -- only the final/nested iframe name.  5 deep only the last iframe. 


                So a bit more detail on operation.  We are focused on and working with valid HTML page
                not all pages may work.  Meaning many frameworks disregard HTML specs  as a suggestion 
                to possibly adheare to.  Meaning instead of a <label/><input...>  they will use only div 
                elements and rely on css properties.  Actually a bad idea.  Example why not:

                https://www.youtube.com/watch?v=YAqRQoN8ykI

                Hat's off and all respect to Keven Powell, while i doubt he would say it, he really is
                Mr. CSS Extrodinare :)

                Net effect many elements have or possess properties that only work if used as designed 
                some things break or are ackward to get things working properly.


                Most all of the controls should sipmely work if it is a valid HTML page.  If things don't work
                and if you have some div elements then a data-testid and it should work, or an id property 
                should work but hopefullly as well.


Questions:      Any issues/questions/suggestions you can get me at:
    
                dano2k3@hotmail.com
                
                                



