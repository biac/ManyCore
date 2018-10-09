﻿using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
#if WINDOWS_UWP
using Windows.Storage;
#endif

namespace ManyCore0404.CellularAutomaton.Logic
{
  public class RuleLoader
  {
    public static async Task<Rule> LoadAsync(string ruleTableFileName)
    {
      var rule = new Rule();
      string line;

#if WINDOWS_UWP
      var sf = await StorageFile.GetFileFromApplicationUriAsync(
                      new Uri("ms-appx:///Logic/" + ruleTableFileName));
      using (var stream = await sf.OpenStreamForReadAsync())
#else // UNIT TEST
      using (var stream = System.IO.File.OpenRead($".\\Logic\\{ruleTableFileName}"))
#endif
      using (TextReader reader = new StreamReader(stream))
      {
        while (null != (line = await reader.ReadLineAsync()))
        {
          if (Regex.IsMatch(line, "^[0-9]"))
            rule.Add(line);
        }
      }
      return rule;
    }
  }
}
