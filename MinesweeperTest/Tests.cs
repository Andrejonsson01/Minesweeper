using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTest;


[TestClass]
public class Tests
{
    [TestMethod]
    public void TestGameOverWhenClickedOnBomb()
    {
        var field = new Minefield();
        field.SetBomb(0, 0);

        field.StartGame();

        Assert.IsFalse(field.IsGameOver());

        field.RevealLocation(0, 0);
        Assert.IsTrue(field.IsGameOver());
    }

    [TestMethod]
    public void TestFloodFillOutsideBounds()
    {
        var field = new Minefield();

        field.StartGame();

        field.SetBomb(2, 2);

        field.RevealLocation(0, 0);
    }

    [TestMethod]
    public void TestFloodFill()
    {
        var field = new Minefield();

        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        field.StartGame();

        field.RevealLocation(4, 0);
        Assert.IsTrue(field.GetLocation(2, 1).IsRevealed);
        Assert.IsTrue(field.GetLocation(2, 0).IsRevealed);
        Assert.IsTrue(field.GetLocation(3, 0).IsRevealed);
        Assert.IsTrue(field.GetLocation(3, 1).IsRevealed);
        Assert.IsTrue(field.GetLocation(4, 0).IsRevealed);
        Assert.IsTrue(field.GetLocation(4, 1).IsRevealed);
    }

    [TestMethod]
    public void TestGameWon()
    {
        var field = new Minefield();

        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        field.StartGame();

        field.RevealLocation(0, 4);
        field.RevealLocation(0, 3);
        field.RevealLocation(0, 2);
        field.RevealLocation(1, 3);
        field.RevealLocation(1, 2);
        field.RevealLocation(1, 0);
        field.RevealLocation(2, 4);
        field.RevealLocation(2, 3);
        field.RevealLocation(2, 2);
        field.RevealLocation(2, 1);
        field.RevealLocation(2, 0);
        field.RevealLocation(3, 4);
        field.RevealLocation(3, 3);
        field.RevealLocation(3, 2);
        field.RevealLocation(3, 1);
        Assert.IsFalse(field.hasWon());
        field.RevealLocation(3, 0);
        Assert.IsTrue(field.hasWon());
    }

    [TestMethod]
    public void TestLocationDisplayValue()
    {
        var field = new Minefield();

        field.StartGame();

        field.SetBomb(1, 3);
        field.SetBomb(1, 2);
        field.SetBomb(1, 1);

        field.SetBomb(2, 1);
        field.SetBomb(2, 3);

        field.SetBomb(3, 3);
        field.SetBomb(3, 2);
        field.SetBomb(3, 1);
        field.RevealLocation(2,2);
        Assert.AreEqual(8, field.GetLocation(2,2).AdjacentBombCount);

    }

}
