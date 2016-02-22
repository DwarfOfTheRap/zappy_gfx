using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System.Collections;
using System.Collections.Generic;

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
		IGrid sic;
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
		IGrid sic;
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
		IGrid sic = GetMockSquareInstantiationController ();
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
		IGrid sic = GetMockSquareInstantiationController ();
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
		IGrid sic = GetMockSquareInstantiationController ();
		gc.SetSquareInstantiationController (sic);
		//Act
		gc.Init (width, height);
		//Assert
	}

	[Test]
	public void GetVision_Level1_Basic_Tests()
	{
		//Arrange
		GridController gc = new GridController();
		IGrid sic = GetMockSquareInstantiationController ();
		gc.SetSquareInstantiationController(sic);
		sic.Instantiate(0).ReturnsForAnyArgs (GetMockSquare());
		gc.Init (10, 10);

		// Assert
		Assert.AreSame(gc.GetVision (5, 5, Orientation.EAST, 1)[0], gc.GetSquare (5, 5));
		Assert.AreSame(gc.GetVision (5, 5, Orientation.WEST, 1)[0], gc.GetSquare (5, 5));
		Assert.AreSame(gc.GetVision (5, 5, Orientation.SOUTH, 1)[0], gc.GetSquare (5, 5));
		Assert.AreSame(gc.GetVision (5, 5, Orientation.NORTH, 1)[0], gc.GetSquare (5, 5));
	}

	[Test]
	public void GetVision_Level1_Modulo_Tests()
	{
		//Arrange
		GridController gc = new GridController();
		IGrid sic = GetMockSquareInstantiationController ();
		gc.SetSquareInstantiationController(sic);
		sic.Instantiate(0).ReturnsForAnyArgs (GetMockSquare());
		gc.Init (10, 10);

		Assert.AreSame(gc.GetVision (0, 0, Orientation.EAST, 1)[2], gc.GetSquare (0, 1));
		Assert.AreSame(gc.GetVision (0, 0, Orientation.WEST, 1)[2], gc.GetSquare (0, 9));
		Assert.AreSame(gc.GetVision (0, 0, Orientation.SOUTH, 1)[2], gc.GetSquare (9, 0));
		Assert.AreSame(gc.GetVision (0, 0, Orientation.NORTH, 1)[2], gc.GetSquare (1, 0));
			
		Assert.AreSame(gc.GetVision (9, 9, Orientation.EAST, 1)[2], gc.GetSquare (9, 0));
		Assert.AreSame(gc.GetVision (9, 9, Orientation.WEST, 1)[2], gc.GetSquare (9, 8));
		Assert.AreSame(gc.GetVision (9, 9, Orientation.SOUTH, 1)[2], gc.GetSquare (0, 9));
		Assert.AreSame(gc.GetVision (9, 9, Orientation.NORTH, 1)[2], gc.GetSquare (8, 9));
	}

	[Test]
	public void GetVision_Level3_North()
	{
		//Arrange
		GridController gc = new GridController();
		IGrid sic = GetMockSquareInstantiationController ();
		gc.SetSquareInstantiationController(sic);
		sic.Instantiate(0).ReturnsForAnyArgs (GetMockSquare());
		gc.Init (10, 10);

		// Act
		List<ISquare> lst = new List<ISquare>(gc.GetVision (0, 0, Orientation.NORTH, 3));

		// Arrange
		Assert.AreEqual (16, lst.Count);
		Assert.AreSame (gc.GetSquare (0, 1), lst.Find(x => x == gc.GetSquare (0, 1)));
		Assert.AreSame (gc.GetSquare (0, 2), lst.Find(x => x == gc.GetSquare (0, 2)));
		Assert.AreSame (gc.GetSquare (0, 3), lst.Find(x => x == gc.GetSquare (0, 3)));
		Assert.AreSame (gc.GetSquare (9, 2), lst.Find(x => x == gc.GetSquare (9, 2)));
		Assert.AreSame (gc.GetSquare (1, 2), lst.Find(x => x == gc.GetSquare (1, 2)));
		Assert.AreSame (gc.GetSquare (8, 3), lst.Find(x => x == gc.GetSquare (8, 3)));
		Assert.AreSame (gc.GetSquare (9, 3), lst.Find(x => x == gc.GetSquare (9, 3)));
		Assert.AreSame (gc.GetSquare (1, 3), lst.Find(x => x == gc.GetSquare (1, 3)));
		Assert.AreSame (gc.GetSquare (2, 3), lst.Find(x => x == gc.GetSquare (2, 3)));
	}

	[Test]
	public void GetVision_Level3_South()
	{
		//Arrange
		GridController gc = new GridController();
		IGrid sic = GetMockSquareInstantiationController ();
		gc.SetSquareInstantiationController(sic);
		sic.Instantiate(0).ReturnsForAnyArgs (GetMockSquare());
		gc.Init (10, 10);
		
		// Act
		List<ISquare> lst = new List<ISquare>(gc.GetVision (0, 0, Orientation.SOUTH, 3));
		
		// Arrange
		Assert.AreEqual (16, lst.Count);
		Assert.AreSame (gc.GetSquare (0, 9), lst.Find(x => x == gc.GetSquare (0, 9)));
		Assert.AreSame (gc.GetSquare (9, 8), lst.Find(x => x == gc.GetSquare (9, 8)));
		Assert.AreSame (gc.GetSquare (0, 8), lst.Find(x => x == gc.GetSquare (0, 8)));
		Assert.AreSame (gc.GetSquare (1, 8), lst.Find(x => x == gc.GetSquare (1, 8)));
		Assert.AreSame (gc.GetSquare (8, 7), lst.Find(x => x == gc.GetSquare (8, 7)));
		Assert.AreSame (gc.GetSquare (9, 7), lst.Find(x => x == gc.GetSquare (9, 7)));
		Assert.AreSame (gc.GetSquare (0, 7), lst.Find(x => x == gc.GetSquare (0, 7)));
		Assert.AreSame (gc.GetSquare (1, 7), lst.Find(x => x == gc.GetSquare (1, 7)));
		Assert.AreSame (gc.GetSquare (2, 7), lst.Find(x => x == gc.GetSquare (2, 7)));
	}

	[Test]
	public void GetVision_Level3_East()
	{
		//Arrange
		GridController gc = new GridController();
		IGrid sic = GetMockSquareInstantiationController ();
		gc.SetSquareInstantiationController(sic);
		sic.Instantiate(0).ReturnsForAnyArgs (GetMockSquare());
		gc.Init (10, 10);
		
		// Act
		List<ISquare> lst = new List<ISquare>(gc.GetVision (0, 0, Orientation.EAST, 3));
		
		// Arrange
		Assert.AreEqual (16, lst.Count);
		Assert.AreSame (gc.GetSquare (1, 0), lst.Find(x => x == gc.GetSquare (1, 0)));
		Assert.AreSame (gc.GetSquare (2, 9), lst.Find(x => x == gc.GetSquare (2, 9)));
		Assert.AreSame (gc.GetSquare (2, 0), lst.Find(x => x == gc.GetSquare (2, 0)));
		Assert.AreSame (gc.GetSquare (2, 1), lst.Find(x => x == gc.GetSquare (2, 1)));
		Assert.AreSame (gc.GetSquare (3, 8), lst.Find(x => x == gc.GetSquare (3, 8)));
		Assert.AreSame (gc.GetSquare (3, 9), lst.Find(x => x == gc.GetSquare (3, 9)));
		Assert.AreSame (gc.GetSquare (3, 0), lst.Find(x => x == gc.GetSquare (3, 0)));
		Assert.AreSame (gc.GetSquare (3, 1), lst.Find(x => x == gc.GetSquare (3, 1)));
		Assert.AreSame (gc.GetSquare (3, 2), lst.Find(x => x == gc.GetSquare (3, 2)));
	}

	[Test]
	public void GetVision_Level3_West()
	{
		//Arrange
		GridController gc = new GridController();
		IGrid sic = GetMockSquareInstantiationController ();
		gc.SetSquareInstantiationController(sic);
		sic.Instantiate(0).ReturnsForAnyArgs (GetMockSquare());
		gc.Init (10, 10);
		
		// Act
		List<ISquare> lst = new List<ISquare>(gc.GetVision (0, 0, Orientation.WEST, 3));
		
		// Arrange
		Assert.AreEqual (16, lst.Count);
		Assert.AreSame (gc.GetSquare (9, 0), lst.Find(x => x == gc.GetSquare (9, 0)));
		Assert.AreSame (gc.GetSquare (8, 9), lst.Find(x => x == gc.GetSquare (8, 9)));
		Assert.AreSame (gc.GetSquare (8, 0), lst.Find(x => x == gc.GetSquare (8, 0)));
		Assert.AreSame (gc.GetSquare (8, 1), lst.Find(x => x == gc.GetSquare (8, 1)));
		Assert.AreSame (gc.GetSquare (7, 8), lst.Find(x => x == gc.GetSquare (7, 8)));
		Assert.AreSame (gc.GetSquare (7, 9), lst.Find(x => x == gc.GetSquare (7, 9)));
		Assert.AreSame (gc.GetSquare (7, 0), lst.Find(x => x == gc.GetSquare (7, 0)));
		Assert.AreSame (gc.GetSquare (7, 1), lst.Find(x => x == gc.GetSquare (7, 1)));
		Assert.AreSame (gc.GetSquare (7, 2), lst.Find(x => x == gc.GetSquare (7, 2)));
	}

	private ISquare GetMockSquare ()
	{
		return Substitute.For<ISquare> ();
	}

	private IGrid GetMockSquareInstantiationController ()
	{
		return Substitute.For<IGrid> ();
	}
}
