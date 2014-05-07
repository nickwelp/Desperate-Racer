using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BuildingGenerator))]

public class BuildingGeneratorEditor : Editor
{

	GUIStyle MenuButtons;
	[MenuItem("Building Generator/Create New Building")]	
	static void CreateNewBuilding ()
	{
		GameObject NewBuilding = new GameObject ("New Building");
		NewBuilding.AddComponent ("BuildingGenerator");

		NewBuilding.tag = "BG Building";
	}

	
	
	public override void OnInspectorGUI ()
		{


			MenuButtons = new GUIStyle("button");
			
			MenuButtons.normal.textColor = Color.blue;
			MenuButtons.fontStyle = FontStyle.Bold;
			MenuButtons.border.top = 5;
			MenuButtons.border.left = 5;
			MenuButtons.border.bottom = 5;
			MenuButtons.border.right = 5;

			GUIStyle regen = new GUIStyle("button");
			regen.normal.textColor = Color.red;
				
				BuildingGenerator BC = (BuildingGenerator)target;
	
				if (BC.AutoGen) {
						if (GUILayout.Button ("Auto Gen On",MenuButtons)) {
								BC.AutoGen = false;
						}
				} else {
			if (GUILayout.Button ("Auto Gen Off",MenuButtons)) {
								BC.AutoGen = true;
						}
				}
				EditorGUILayout.Space ();
		
		EditorGUILayout.Space ();
		EditorGUILayout.LabelField ("Add Base");
		BC.addbase =		EditorGUILayout.Toggle (BC.addbase);
		EditorGUILayout.Space ();
				BC.BuildingCount = EditorGUILayout.IntSlider ("Total Buildings", BC.BuildingCount, 1, 100);
				/*
		EditorGUILayout.Space ();
		BC.UndergroundExtension = EditorGUILayout.Slider ("Extend Underground", BC.UndergroundExtension, 0, 10);
		
		EditorGUILayout.Space ();
*/

				EditorGUILayout.Space ();
				BC.windowextrude = EditorGUILayout.Slider ("Window Depth", BC.windowextrude, 0, 2);
		
				EditorGUILayout.Space ();

				EditorGUILayout.Space ();

				if (BC.ShowMainData == true) {
			if (GUILayout.Button ("Hide Main Data",MenuButtons)) {
								BC.ShowMainData = false;
						}
				
						EditorGUILayout.LabelField ("Floor Plan");

						BC.MinWidth = EditorGUILayout.IntSlider ("Min Width", BC.MinWidth, 1, 100);
						BC.MaxWidth = EditorGUILayout.IntSlider ("Max Width", BC.MaxWidth, BC.MinWidth, 100);
						EditorGUILayout.Space ();

						BC.MinDepth = EditorGUILayout.IntSlider ("Min Depth", BC.MinDepth, 1, 100);
						BC.MaxDepth = EditorGUILayout.IntSlider ("Max Depth", BC.MaxDepth, BC.MinDepth, 100);
						EditorGUILayout.Space ();

						// Set Floor Count Range
						EditorGUILayout.LabelField ("Floor Count");

						BC.MinFloors = EditorGUILayout.IntSlider ("Min Floors", BC.MinFloors, 1, 100);
						BC.MaxFloors = EditorGUILayout.IntSlider ("Max Floors", BC.MaxFloors, BC.MinFloors, 100);

				} else {
			if (GUILayout.Button ("Show Main Data",MenuButtons)) {
								BC.ShowMainData = true;
						}
				}
		
				//GROUND FLOOR
				if (BC.ShowGFDATA) {
			if (GUILayout.Button ("Hide Ground Floor Data",MenuButtons)) {
								BC.ShowGFDATA = false;
						}
						GroundFloorOptions (BC);
				} else {
			if (GUILayout.Button ("Show Ground Floor Data",MenuButtons)) {
								BC.ShowGFDATA = true;
						}
				}
		
				//SUB FLOOR
				if (BC.ShowSFDATA) {
			if (GUILayout.Button ("Hide Sub Floor Data",MenuButtons)) {
								BC.ShowSFDATA = false;
						}
						SubFloorOptions (BC);
				} else {
			if (GUILayout.Button ("Show Sub Floor Data",MenuButtons)) {
								BC.ShowSFDATA = true;
						}
				}
				//ROOF
				if (BC.ShowROOFDATA) {
			if (GUILayout.Button ("Hide Roof Data",MenuButtons)) {
								BC.ShowROOFDATA = false;
						}
						RoofOptions (BC);
				} else {
			if (GUILayout.Button ("Show Roof Data",MenuButtons)) {
								BC.ShowROOFDATA = true;
						}
				}

		if (GUILayout.Button ("Regenerate", regen)) {
						BC.Generate ();
				}

		}

		void GroundFloorOptions (BuildingGenerator BC)
		{
				BC.GFMinHeight = EditorGUILayout.Slider ("Min Height", BC.GFMinHeight, 1, 100);
				BC.GFMaxHeight = EditorGUILayout.Slider ("Max Height", BC.GFMaxHeight, BC.GFMinHeight, 100);
				EditorGUILayout.Space ();
				EditorGUILayout.LabelField ("Wall Material");
				BC.GFMat = EditorGUILayout.ObjectField (BC.GFMat, (typeof(Material)), false) as Material;
		if (BC.GFMat_UseRNDAltMat == true) {
			if (GUILayout.Button ("Don't Use  Material")) {
				BC.GFMat_UseRNDAltMat = false;
			}
			EditorGUILayout.LabelField ("Random Alternative Material");
			BC.GFMat_Alt = EditorGUILayout.ObjectField (BC.GFMat_Alt, (typeof(Material)), false) as Material;
		} else {
			if (GUILayout.Button ("Use Random Alternative Material")) {
				BC.GFMat_UseRNDAltMat = true;
			}
		}
				
				EditorGUILayout.Space ();


				EditorGUILayout.Space ();
		
				if (BC.UseGroundDoor) {
						if (GUILayout.Button ("Don't Use Doors")) {
								BC.UseGroundDoor = false;
						}
			
						BC.GroundDoorHeight = EditorGUILayout.Slider ("Door Height", BC.GroundDoorHeight, 0, BC.GFMinHeight);
						BC.GroundDoorWidth = EditorGUILayout.Slider ("Door Width", BC.GroundDoorWidth, 0, 20);
			BC.GroundDoorSpacing = EditorGUILayout.Slider ("Door Spacing From Left", BC.GroundDoorSpacing, 0.05f, BC.MinWidth - BC.GroundDoorWidth);
			BC.GroundDoorBottomSpacing = EditorGUILayout.Slider ("Door Bottom Spacing", BC.GroundDoorBottomSpacing, 0.05f, BC.GFMinHeight - BC.GroundDoorHeight);
			
						BC.GroundDoorFront = GUILayout.Toggle (BC.GroundDoorFront, "Front Wall");
						BC.GroundDoorRear = GUILayout.Toggle (BC.GroundDoorRear, "Rear Wall");

						

			
						EditorGUILayout.LabelField ("Door Material");
						BC.GroundDoorMat = EditorGUILayout.ObjectField (BC.GroundDoorMat, (typeof(Material)), false) as Material;
			
						if (BC.GroundDoorMat_UseRNDAltMat == true) {
								if (GUILayout.Button ("Don't Use Material")) {
										BC.GroundDoorMat_UseRNDAltMat = false;
								}
								EditorGUILayout.LabelField ("Random Alternative Material");
								BC.GroundDoorMat_Alt = EditorGUILayout.ObjectField (BC.GroundDoorMat_Alt, (typeof(Material)), false) as Material;
				
						} else {
								if (GUILayout.Button ("Use Random Alternative Material")) {
										BC.GroundDoorMat_UseRNDAltMat = true;
								}
						}
			
			
				} else {
						if (GUILayout.Button ("Use Doors")) {
								BC.UseGroundDoor = true;
						}
				}
		
				EditorGUILayout.Space ();

				/////WINDOW
				/// 
		
				if (BC.UseGroundWindow) {
						if (GUILayout.Button ("Don't Use Windows")) {
								BC.UseGroundWindow = false;
						}
			
						BC.GroundWindowHeight = EditorGUILayout.Slider ("Window Height", BC.GroundWindowHeight, 1, BC.SFMinHeight);
						BC.GroundWindowWidth = EditorGUILayout.Slider ("Window Width", BC.GroundWindowWidth, 1, 20);
						BC.GroundWindowSpacing = EditorGUILayout.Slider ("Window Spacing", BC.GroundWindowSpacing, 0.5f, 20);
			BC.GroundWindowBottomSpacing = EditorGUILayout.Slider ("Window Base Spacing", BC.GroundWindowBottomSpacing, 0.0f, BC.GFMinHeight-BC.GroundWindowHeight);
			

						BC.GroundWindowFront = GUILayout.Toggle (BC.GroundWindowFront, "Front Wall");
						BC.GroundWindowRear = GUILayout.Toggle (BC.GroundWindowRear, "Rear Wall");
			
			
						EditorGUILayout.LabelField ("Window Material");
						BC.GroundWindowMat = EditorGUILayout.ObjectField (BC.GroundWindowMat, (typeof(Material)), false) as Material;
			
						if (BC.GroundWindowMat_UseRNDAltMat == true) {
								if (GUILayout.Button ("Don't Use Material")) {
										BC.GroundWindowMat_UseRNDAltMat = false;
								}
								EditorGUILayout.LabelField ("Random Alternative Material");
								BC.GroundWindowMat_Alt = EditorGUILayout.ObjectField (BC.GroundWindowMat_Alt, (typeof(Material)), false) as Material;
				
						} else {
								if (GUILayout.Button ("Use Random Alternative Material")) {
										BC.GroundWindowMat_UseRNDAltMat = true;
								}
						}
			
			
				} else {
						if (GUILayout.Button ("Use Windows")) {
								BC.UseGroundWindow = true;
						}
				}
		
				EditorGUILayout.Space ();

		}

		void SubFloorOptions (BuildingGenerator BC)
		{
				BC.SFMinHeight = EditorGUILayout.Slider ("Min Height", BC.SFMinHeight, 1, 100);
				BC.SFMaxHeight = EditorGUILayout.Slider ("Max Height", BC.SFMaxHeight, BC.SFMinHeight, 100);
				EditorGUILayout.Space ();
				if (BC.SFMatUseGF) {
						if (GUILayout.Button ("Use Seperate Material")) {
								BC.SFMatUseGF = false;
						}
				} else {
						if (GUILayout.Button ("Use Ground Floor Material")) {
								BC.SFMatUseGF = true;
						}
						EditorGUILayout.LabelField ("Wall Material");
						BC.SFMat = EditorGUILayout.ObjectField (BC.SFMat, (typeof(Material)), false) as Material;

						if (BC.SFMat_UseRNDAltMat == true) {
								if (GUILayout.Button ("Don't Use  Material")) {
										BC.SFMat_UseRNDAltMat = false;
								}
								EditorGUILayout.LabelField ("Random Alternative Material");
								BC.SFMat_Alt = EditorGUILayout.ObjectField (BC.SFMat_Alt, (typeof(Material)), false) as Material;
						} else {
								if (GUILayout.Button ("Use Random Alternative Material")) {
										BC.SFMat_UseRNDAltMat = true;
								}
						}
				}
				EditorGUILayout.Space ();

				if (BC.UseUpperWindow) {
						if (GUILayout.Button ("Don't Use Windows")) {
								BC.UseUpperWindow = false;
						}

						BC.UpperWindowHeight = EditorGUILayout.Slider ("Window Height", BC.UpperWindowHeight, 1, BC.SFMinHeight);
						BC.UpperWindowWidth = EditorGUILayout.Slider ("Window Width", BC.UpperWindowWidth, 1, 20);
			BC.UpperWindowSpacing = EditorGUILayout.Slider ("Window Spacing", BC.UpperWindowSpacing, 0.5f, 20);
			BC.UpperWindowBottomSpacing = EditorGUILayout.Slider ("Window Base Spacing", BC.UpperWindowBottomSpacing, 0.0f, BC.SFMinHeight - BC.UpperWindowHeight);

			BC.UpperWindowFront = GUILayout.Toggle (BC.UpperWindowFront, "Front Wall");
						BC.UpperWindowRear = GUILayout.Toggle (BC.UpperWindowRear, "Rear Wall");

			
						EditorGUILayout.LabelField ("Window Material");
						BC.UpperWindowMat = EditorGUILayout.ObjectField (BC.UpperWindowMat, (typeof(Material)), false) as Material;

						if (BC.UpperWindowMat_UseRNDAltMat == true) {
								if (GUILayout.Button ("Don't Use Material")) {
										BC.UpperWindowMat_UseRNDAltMat = false;
								}
								EditorGUILayout.LabelField ("Random Alternative Material");
								BC.UpperWindowMat_Alt = EditorGUILayout.ObjectField (BC.UpperWindowMat_Alt, (typeof(Material)), false) as Material;

						} else {
								if (GUILayout.Button ("Use Random Alternative Material")) {
										BC.UpperWindowMat_UseRNDAltMat = true;
								}
						}
			
			
				} else {
						if (GUILayout.Button ("Use Windows")) {
								BC.UseUpperWindow = true;
						}
				}
		
		
				EditorGUILayout.Space ();
		
		}

		public void RoofOptions (BuildingGenerator BC)
		{
				BC.RT = (BuildingGenerator.RoofTypes)EditorGUILayout.EnumPopup ("Roof Type", BC.RT);
		
				BC.RoofOverhang = EditorGUILayout.Slider ("Roof Overhang ", BC.RoofOverhang, 0, 10);
				BC.RoofFrontOverhang = EditorGUILayout.Slider ("Roof Front Overhang ", BC.RoofFrontOverhang, BC.RoofOverhang, 20);
				
		BC.Roof_Height = EditorGUILayout.Slider ("Roof Height", BC.Roof_Height, 0, 10);

		if (BC.RT == BuildingGenerator.RoofTypes.Hip) {
						BC.HipSize = EditorGUILayout.Slider ("Roof Hip length", BC.HipSize, 0, BC.MinDepth);
				}
				EditorGUILayout.LabelField ("Roof Material");
				BC.RoofMat = EditorGUILayout.ObjectField (BC.RoofMat, (typeof(Material)), false) as Material;
		
				if (BC.RoofMat_UseRNDAltMat == true) {
						if (GUILayout.Button ("Don't Use  Material")) {
								BC.RoofMat_UseRNDAltMat = false;
						}
						EditorGUILayout.LabelField ("Random Alternative Material");
						BC.RoofMat_Alt = EditorGUILayout.ObjectField (BC.RoofMat_Alt, (typeof(Material)), false) as Material;
				} else {
						if (GUILayout.Button ("Use Random Alternative Material")) {
								BC.RoofMat_UseRNDAltMat = true;
						}
				}

		}
}
