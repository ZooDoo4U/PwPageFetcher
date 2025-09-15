                                                              import { Locator } from "@playwright/test";

export async function setDateInput(locator: Locator, dateValue: string | Date): Promise<void>
{
    const dateStringValue  =
        typeof dateValue === 'string' ?
            new Date(dateValue).toISOString().split('T')[0] : 
            dateValue.toISOString().split('T')[0];
            
    await locator.fill(dateStringValue);
};

 
export async function getOptionValues(locator: Locator): Promise<string[]> 
{
    // Check if the locator points to a <datalist> or <select> element
    const tagName = await locator.evaluate(el => el.tagName.toLowerCase());
  
    if (tagName === 'datalist' || tagName === 'select') 
    {
      // Get all option elements within the datalist or select
      const optionLocators = locator.locator('option');
  
      // Extract the 'value' attribute or text content from each option
      const values = await optionLocators.evaluateAll(options => options.map(option => 
      {    
          return option.getAttribute('value') || option.textContent || '';
      }));
      return values;
    } 
    else 
    {
      throw new Error('Provided locator is neither a <datalist> nor a <select> element.');
    }
  }
 

export const sleep = async (ms) => new Promise(resolve => setTimeout(resolve, ms));

export const highlightElement = async (locator:Locator, color = 'cyan', times = 3, durationMs = 1500) =>
{
  await locator.waitFor();
  try
  {    
    await locator.scrollIntoViewIfNeeded();
    await locator.focus();
  }
  catch
  {
    
  }
  
  
  const originalStyle = await locator.evaluate(el => el.getAttribute('style') || '');

  for (let i = 0; i < times * 2; i++) 
  {
    const highlightOn = i % 2 === 0;
    await locator.evaluate((el, [color, highlightOn, originalStyle]) => {
      el.setAttribute('style', highlightOn ? `background-color: ${color}; ${originalStyle}` : originalStyle);
    }, [color, highlightOn, originalStyle]);
    await locator.page().waitForTimeout(durationMs / (times * 2));
  }

  // Ensure original style is restored
  await locator.evaluate((el, originalStyle) => 
  {
    el.setAttribute('style', originalStyle);
  }, originalStyle);
}