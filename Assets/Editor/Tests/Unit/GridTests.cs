using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System.Collections;

[TestFixture]
public class GridTests {
	
	[Test]
	public void ClearGrid_NotNull_Grid ()
	{
		//Arrange
		GridController gc = new GridController();
		ISquare sq1 = GetMockSquare ();
		ISquare sq2 = GetMockSquare ();
		ISquare sq3 = GetMockSquare ();
		ISquare sq4 = GetMockSquare ();
		ISquare sq5 = GetMockSquare ();
		ISquare[] grid = {sq1, sq2, sq3, sq4, sq5};
		ISquare[] expected_result = null;
		//Act
		grid = gc.ClearGrid (grid);
		//Assert
		sq1.Received ().Destroy ();
		sq2.Received ().Destroy ();
		sq3.Received ().Destroy ();
		sq4.Received ().Destroy ();
		sq5.Received ().Destroy ();
		Assert.AreEqual (expected_result, grid);
	}

	[Test]
	public void ClearGrid_Null_Grid ()
	{
		//Arrange
		GridController gc = new GridController();
		ISquare[] grid = null;
		ISquare[] expected_result = null;
		//Act
		grid = gc.ClearGrid (grid);
		//Assert
		Assert.AreEqual (expected_result, grid);
	}


	[Test]
	public void GetSquare_Correct_Coordinates ()
	{
		//Arrange
		GridController gc = new GridController ();
		int height = 10;
		int width = 10;
		ISquareInstantiationController sic;
		ISquare result;
		ISquare expected_result;
		//Act
		sic = GetMockSquareInstantiationController ();
		gc.SetSquareInstantiationController (sic);
		gc.Init (width, height);
		expected_result = gc.grid [33];
		result = gc.GetSquare (3, 3);
		//Assert
		Assert.AreEqual (expected_result, result);
	}
	
	[Test]
	[ExpectedException(typeof(GridController.GridNotInitializedException))]
	public void GetSquare_Null_Grid ()
	{
		//Arrange
		GridController gc = new GridController ();
		gc.GetSquare (12, 12);
	}

	[Test]
	[ExpectedException(typeof(GridController.GridOutOfBoundsException))]
	public void GetSquare_Incorrect_Coordinates ()
	{
		//Arrange
		GridController gc = new GridController ();
		int height = 10;
		int width = 10;
		ISquareInstantiationController sic;
		//Act
		sic = GetMockSquareInstantiationController ();
		gc.SetSquareInstantiationController (sic);
		gc.Init (width, height);
		gc.GetSquare (12, 12);
	}

	[Test]
	public void Init_Correct_Grid_Size ()
	{
		//Arrange
		GridController gc = new GridController ();
		int height = 10;
		int width = 10;
		ISquareInstantiationController sic = GetMockSquareInstantiationController ();
		gc.SetSquareInstantiationController (sic);
		//Act
		gc.Init (width, height);
		//Assert
		Assert.IsNotEmpty (gc.grid);
		int i = 0;
		while (i < gc.grid.Length)
			++i;
		Assert.AreEqual(100, i);
	}

	[Test]
	public void Init_Checkboard ()
	{
		//Arrange
		GridController gc = new GridController ();
		int height = 10;
		int width = 10;
		ISquareInstantiationController sic = GetMockSquareInstantiationController ();
		gc.SetSquareInstantiationController (sic);
		//Act
		gc.Init (width, height);
		//Assert
		Assert.IsNotEmpty (gc.grid);
		int i = 0;
		while (i < gc.grid.Length)
			++i;
		Assert.AreEqual(100, i);
	}

	[Test]
	[ExpectedException(typeof(GridController.GridIllegalSizeException))]
	public void Init_Negative_Grid_Size ()
	{
		//Arrange
		GridController gc = new GridController ();
		int height = -10;
		int width = 10;
		ISquareInstantiationController sic = GetMockSquareInstantiationController ();
		gc.SetSquareInstantiationController (sic);
		//Act
		gc.Init (width, height);
		//Assert
	}

	private ISquare GetMockSquare ()
	{
		return Substitute.For<ISquare> ();
	}

	private ISquareInstantiationController GetMockSquareInstantiationController ()
	{
		return Substitute.For<ISquareInstantiationController> ();
	}
}
