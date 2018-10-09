﻿using ManyCore0403.CellularAutomaton.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManyCore0403.CellularAutomatonTests
{
  [TestClass]
  public class RuleTest
  {
    [TestMethod]
    public void NextTest_000000()
    {
      var rule = new Rule();
      rule.Add("000000");
      Assert.AreEqual(0, rule.Next(0, 0, 0, 0, 0));
    }

    [TestMethod]
    public void NextTest_702525()
    {
      var rule = new Rule();
      rule.Add("702525");
      Assert.AreEqual(5, rule.Next(7, 0, 2, 5, 2));
      Assert.AreEqual(5, rule.Next(7, 2, 0, 2, 5));
      Assert.AreEqual(5, rule.Next(7, 5, 2, 0, 2));
      Assert.AreEqual(5, rule.Next(7, 2, 5, 2, 0));
    }



    [TestMethod]
    public void AddTest_000000()
    {
      var rule = new Rule();
      rule.Add("000000");
      Assert.AreEqual(1, rule.Count); // "00000"は回転しても同じなので、追加されるルールは1つだけ
    }

  }
}
