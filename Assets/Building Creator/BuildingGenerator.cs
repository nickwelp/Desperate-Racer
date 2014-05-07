using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode()]
public class BuildingGenerator : MonoBehaviour
{
		public class SubMesh
		{
				public List<int> tris = new List<int> ();
		}
	
		public enum RoofTypes
		{
				Flat = 0,
				Gable = 1,
				Hip = 2
	}



		public float UndergroundExtgenerateension = 0;
		public int BuildingCount;
		protected List<Vector3> verts = new List<Vector3> ();
		protected List<Vector2> uv = new List<Vector2> ();
		protected List<Material> MatList = new List<Material> ();
		protected List<SubMesh> MeshList = new List<SubMesh> ();
		protected Mesh mesh;
	public float HipSize = 0;
		public RoofTypes RT = RoofTypes.Flat;
		public bool ShowMainData = false;
		public bool AutoGen = false;
		public bool ShowGFDATA = false;
		public bool ShowSFDATA = false;
		public int MinWidth ;
		public int MaxWidth ;
		public int MinDepth ;
		public int MaxDepth ;
		public int MinFloors = 1;
		public int MaxFloors = 1;
		public float GFMinHeight;
		public float RoofFrontOverhang;
		public float Roof_Height ;
		public bool ShowROOFDATA = false;
		public float GFMaxHeight ;
		public bool GFMat_UseRNDAltMat ;
		public bool SFMatUseGF ;
		public bool SFMat_UseRNDAltMat ;
		public bool RoofMat_UseRNDAltMat ;
		public float RoofOverhang ;
		public float SFMinHeight ;
		public float SFMaxHeight ;
		public Material GFMat_Alt ;
		public Material SFMat_Alt ;
		public Material GFMat ;
		public Material SFMat ;
		public Material RoofMat ;
		public Material RoofMat_Alt ;
		public float UpperWindowHeight;
		public float UpperWindowWidth;
		public float UpperWindowSpacing;
		public bool UseUpperWindow;
		public bool UpperWindowFront;
		public bool UpperWindowRear;
		public bool UpperWindowLeft;
		public bool UpperWindowRight;
		public bool UpperWindowMat_UseRNDAltMat ;
		public Material UpperWindowMat;
		public GameObject UpperWindowPrefab;
		public Material UpperWindowMat_Alt ;
	public GameObject UpperWindowPrefab_Alt ;
	public float GroundWindowBottomSpacing;
	public float UpperWindowBottomSpacing;
	
	public float GroundWindowHeight;
		public float GroundWindowWidth;
		public float GroundWindowSpacing;
		public bool UseGroundWindow;
		public bool GroundWindowFront;
		public bool GroundWindowRear;
		public bool GroundWindowLeft;
		public bool GroundWindowRight;
		public bool GroundWindowMat_UseRNDAltMat ;
		public Material GroundWindowMat;
		public GameObject GroundWindowPrefab;
		public Material GroundWindowMat_Alt ;
		public GameObject GroundWindowPrefab_Alt ;
		public float GroundDoorHeight;
		public float GroundDoorWidth;
		public float GroundDoorSpacing;
		public float GroundDoorBottomSpacing;
		public bool UseGroundDoor;
		public bool GroundDoorFront;
		public bool GroundDoorRear;
		public bool GroundDoorLeft;
		public bool GroundDoorRandomPos;
		public bool GroundDoorRight;
		public bool GroundDoorMat_UseRNDAltMat ;
		public Material GroundDoorMat;
		public GameObject GroundDoorPrefab;
		public Material GroundDoorMat_Alt ;
		public GameObject GroundDoorPrefab_Alt ;
		public float windowextrude = 0.01f;
	public bool addbase = true;
		// Use this for initialization
		void Start ()
		{
				if (AutoGen == true) {
						Generate ();
				}
		}

		// Update is called once per frame
		public void Update ()
		{

				
		}

		public void Generate ()
		{
				mesh = new Mesh ();
				GetComponent<MeshFilter> ().mesh = mesh;

				MeshList.Clear ();

				MatList.Clear ();
				verts.Clear ();
				uv.Clear ();

				int i = 0;
				while (i < 12) {
						MeshList.Add (new SubMesh ());
						MatList.Add (GFMat);
						i++;		
				}

				float XPos = 0;
				int BuildingNumber = 0;
				while (BuildingNumber < BuildingCount) {






						int floors = UnityEngine.Random.Range (MinFloors, MaxFloors + 1);
						//debug.Log (floors.ToString ());

						int CurrentFloor = 0;

		
						int ThisWidth = UnityEngine.Random.Range (MinWidth, MaxWidth + 1);		
						int ThisDepth = UnityEngine.Random.Range (MinDepth, MaxDepth + 1);
		
						float GF_Height = UnityEngine.Random.Range (GFMinHeight, GFMaxHeight + 1.0f);
						float SF_Height = UnityEngine.Random.Range (SFMinHeight, SFMaxHeight + 1.0f);

				
						int index = 0;
						float H = 0;
						int SMID = 0;
						int LastWall_SMID = 0;
		
		
		
						MatList [0] = GFMat;
						MatList [1] = SFMat;
						MatList [2] = SFMat_Alt;
						MatList [3] = RoofMat;
						MatList [4] = RoofMat_Alt;
			
						MatList [5] = UpperWindowMat;
						MatList [6] = UpperWindowMat_Alt;
			
						MatList [7] = GroundWindowMat;
						MatList [8] = GroundWindowMat_Alt;

			
			
						MatList [9] = GroundDoorMat;
						MatList [10] = GroundDoorMat_Alt;
						MatList [11] = GFMat_Alt;
			
			
						float addHeight = 0;

						if (UndergroundExtgenerateension > 0) {
				
								////////////////////
				
						}
			int gfmid= 0;
			if (GFMat_UseRNDAltMat) {
				int c = UnityEngine.Random.Range (1, 3);
				
				if (c == 1) {
					gfmid = 0;
				} else {
					gfmid = 11;
				}
			}else{
				gfmid = 0;
			}
			LastWall_SMID = gfmid;
			if(addbase){
				index = verts.Count;
				
				verts.Add (new Vector3 (XPos + ThisWidth,0, 0));
				uv.Add (new Vector2 ((ThisWidth / GFMaxHeight), 0));
				
				verts.Add (new Vector3 (XPos +0,0, 0));
				
				uv.Add (new Vector2 (0, 0));
				
				verts.Add (new Vector3 (XPos +ThisWidth,0, ThisDepth));
				
				uv.Add (new Vector2 ((ThisWidth / GFMaxHeight), (ThisDepth / GFMaxHeight)));
				
				verts.Add (new Vector3 (XPos +0,0, ThisDepth));
				
				uv.Add (new Vector2 (0, (ThisDepth / GFMaxHeight)));
				//FinalX = StartX + WindowWidth;								
				
				MeshList [LastWall_SMID].tris.Add (index + 1);
				MeshList [LastWall_SMID].tris.Add (index + 2);	
				MeshList [LastWall_SMID].tris.Add (index + 3);
				MeshList [LastWall_SMID].tris.Add (index + 2);
				MeshList [LastWall_SMID].tris.Add (index + 1);
				MeshList [LastWall_SMID].tris.Add (index + 0);
			}

						int subfloormat = 0;
						if (SFMat_UseRNDAltMat) {
				
				
				
								subfloormat = UnityEngine.Random.Range (1, 3);
				
						} else {
								subfloormat = 1;
						}
						while (CurrentFloor < floors) {
								SMID = 0;
				
								if (CurrentFloor < 1) {
					SMID = gfmid;
										

								} else {
										if (SFMatUseGF) {
												SMID = gfmid;
										} else {
										

												SMID = subfloormat;
												
										}
								}

								addHeight = 0;

								if (CurrentFloor < 1) {
										addHeight = GF_Height;
								} else {
										addHeight = SF_Height;
								}


								// FRONT WALL
				
								LastWall_SMID = SMID;
								if ((CurrentFloor > 0 && UseUpperWindow && UpperWindowFront) || (CurrentFloor < 1 && ((UseGroundWindow && GroundWindowFront)||UseGroundDoor && GroundDoorFront ))) {
										{
						
						
												float WindowHeight = 0;
												float WindowWidth = 0;
												float WindowSpacing = 0;
						float WindowBottomSpacing =0;
												if (CurrentFloor > 0) {
														WindowHeight = UpperWindowHeight;
														WindowWidth = UpperWindowWidth;
														WindowSpacing = UpperWindowSpacing;
							WindowBottomSpacing = UpperWindowBottomSpacing;

							if ( UseUpperWindow == false||UpperWindowRear ==false)
							{
								WindowSpacing = ThisWidth*2;
							}
												} else {
														WindowHeight = GroundWindowHeight;
														WindowWidth = GroundWindowWidth;
														WindowSpacing = GroundWindowSpacing;
							WindowBottomSpacing = GroundDoorBottomSpacing;

							if ( UseGroundWindow == false||GroundWindowRear ==false)
							{
								WindowSpacing = ThisWidth*2;
							}
												}
												int Count = 1;
						
												float CurrentWidth = WindowWidth;
												float SafeWidth = WindowWidth;
												//int SafeCount = 1;
						
												float FinalX = XPos;
						
												float DoorSpace = 0;
												if (CurrentWidth + WindowSpacing > (ThisWidth)) {

												} else {
														while (CurrentWidth + WindowSpacing < (ThisWidth)) {
																SafeWidth = CurrentWidth;
								
																CurrentWidth += WindowSpacing + WindowWidth;
																Count++;
														}
												}											
						
												float StartX = FinalX + ((float)(ThisWidth) / 2) - (SafeWidth / 2);
												float StartY = WindowBottomSpacing ;
						
						
												if ((CurrentFloor == 0 && UseGroundDoor && GroundDoorFront)) {
							
														index = verts.Count;
							
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing, H, 0));
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing) / addHeight, 0));
														verts.Add (new Vector3 (FinalX + 0, H, 0));
							
														uv.Add (new Vector2 (FinalX / addHeight, 0));
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing, H + StartY, 0));
							
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing) / addHeight, StartY / addHeight));
														verts.Add (new Vector3 (FinalX + 0, H + StartY, 0));
							
														uv.Add (new Vector2 (FinalX / addHeight, StartY / addHeight));
							
							
														MeshList [SMID].tris.Add (index + 0);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
							
														index = verts.Count;
							
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing - DoorSpace, H + StartY + WindowHeight, 0));
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing) / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX + 0, H + StartY + WindowHeight, 0));
														uv.Add (new Vector2 (FinalX / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing - DoorSpace, H + addHeight, 0));
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing) / addHeight, 1));
							
														verts.Add (new Vector3 (FinalX + 0, H + addHeight, 0));
														uv.Add (new Vector2 (FinalX / addHeight, 1));
							
							
														MeshList [SMID].tris.Add (index + 0);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
							
							
							
														////////////////////////////////////////////////////////////////////////////////////////////////
							
							
							
							
														index = verts.Count;
							
														verts.Add (new Vector3 (FinalX + ThisWidth, H, 0));
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, 0));
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing + GroundDoorWidth, H, 0));
							
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing + GroundDoorWidth) / addHeight, 0));
														verts.Add (new Vector3 (FinalX + ThisWidth, H + StartY, 0));
							
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, StartY / addHeight));
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing + GroundDoorWidth, H + StartY, 0));
							
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing + GroundDoorWidth) / addHeight, StartY / addHeight));
							
							
														MeshList [SMID].tris.Add (index + 0);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
							
														index = verts.Count;
							
														verts.Add (new Vector3 (FinalX + ThisWidth - DoorSpace, H + StartY + WindowHeight, 0));
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing + GroundDoorWidth, H + StartY + WindowHeight, 0));
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing + GroundDoorWidth) / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX + ThisWidth - DoorSpace, H + addHeight, 0));
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, 1));
							
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing + GroundDoorWidth, H + addHeight, 0));
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing + GroundDoorWidth) / addHeight, 1));
							
							
														MeshList [SMID].tris.Add (index + 0);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
							
							
							
							
							
							
												} else {
							
														index = verts.Count;
							
														verts.Add (new Vector3 (FinalX + ThisWidth, H, 0));
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, 0));
														verts.Add (new Vector3 (FinalX + 0, H, 0));
							
														uv.Add (new Vector2 (FinalX / addHeight, 0));
														verts.Add (new Vector3 (FinalX + ThisWidth, H + StartY, 0));
							
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, StartY / addHeight));
														verts.Add (new Vector3 (FinalX + 0, H + StartY, 0));
							
														uv.Add (new Vector2 (FinalX / addHeight, StartY / addHeight));
							
							
														MeshList [SMID].tris.Add (index + 0);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
							
														index = verts.Count;
							
														verts.Add (new Vector3 (FinalX + ThisWidth - DoorSpace, H + StartY + WindowHeight, 0));
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX + 0, H + StartY + WindowHeight, 0));
														uv.Add (new Vector2 (FinalX / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX + ThisWidth - DoorSpace, H + addHeight, 0));
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, 1));
							
														verts.Add (new Vector3 (FinalX + 0, H + addHeight, 0));
														uv.Add (new Vector2 (FinalX / addHeight, 1));
							
							
														MeshList [SMID].tris.Add (index + 0);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
							
												}
						
												Count = 0;
						
												///DO WINDOWS
												/// 
												bool doorPlaced = false;
												while (StartX + WindowWidth < XPos + ThisWidth) {
							
							
														if ((CurrentFloor == 0 && UseGroundDoor && GroundDoorFront) && StartX + WindowWidth > XPos + GroundDoorSpacing && doorPlaced == false) {
								
																doorPlaced = true;
								
								
																index = verts.Count;
								
																verts.Add (new Vector3 (XPos + GroundDoorSpacing, H + StartY, 0));
																uv.Add (new Vector2 ((XPos + GroundDoorSpacing) / addHeight, StartY / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + StartY, 0));							
																uv.Add (new Vector2 (FinalX / addHeight, StartY / addHeight));
								
																verts.Add (new Vector3 (XPos + GroundDoorSpacing, H + StartY + WindowHeight, 0));							
																uv.Add (new Vector2 ((XPos + GroundDoorSpacing) / addHeight, (StartY + WindowHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + StartY + WindowHeight, 0));
								
																uv.Add (new Vector2 (FinalX / addHeight, (StartY + WindowHeight) / addHeight));
								
								
								
																MeshList [LastWall_SMID].tris.Add (index + 0);
																MeshList [LastWall_SMID].tris.Add (index + 1);	
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
																FinalX = XPos + GroundDoorSpacing;
								
																//////////////////////////////////////////////////////////////////////
																/// 
								
								
																index = verts.Count;
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing + GroundDoorHeight, 0));
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing + GroundDoorHeight, 0));							
																uv.Add (new Vector2 ((FinalX) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + addHeight, 0));							
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + addHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + addHeight, 0));
								
																uv.Add (new Vector2 ((FinalX) / addHeight, (H + addHeight) / addHeight));
																//FinalX = FinalX;
								
								
																MeshList [LastWall_SMID].tris.Add (index + 0);
																MeshList [LastWall_SMID].tris.Add (index + 1);	
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
								
																index = verts.Count;
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H, 0));
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, H / addHeight));
								
																verts.Add (new Vector3 (FinalX, H, 0));							
																uv.Add (new Vector2 ((FinalX) / addHeight, H / addHeight));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing, 0));							
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + GroundDoorBottomSpacing) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing, 0));
								
																uv.Add (new Vector2 ((FinalX) / addHeight, (H + GroundDoorBottomSpacing) / addHeight));
																//		FinalX = FinalX;
								
								
								
																MeshList [LastWall_SMID].tris.Add (index + 0);
																MeshList [LastWall_SMID].tris.Add (index + 1);	
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
								
								
								
								
																/////  WINDOW
																index = verts.Count;
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing, windowextrude));
																uv.Add (new Vector2 (1, 0));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing, windowextrude));
								
																uv.Add (new Vector2 (0, 0));
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing + GroundDoorHeight, windowextrude));
								
																uv.Add (new Vector2 (1, 1));
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing + GroundDoorHeight, windowextrude));
								
																uv.Add (new Vector2 (0, 1));
								
																if (GroundDoorMat_UseRNDAltMat) {
																		SMID = UnityEngine.Random.Range (9, 11);
																} else {
																		SMID = 9;
																}
								
																MeshList [SMID].tris.Add (index + 0);
																MeshList [SMID].tris.Add (index + 1);
																MeshList [SMID].tris.Add (index + 2);
																MeshList [SMID].tris.Add (index + 3);
																MeshList [SMID].tris.Add (index + 2);
																MeshList [SMID].tris.Add (index + 1);
								
								
																//  WINDOW FRAMES
								
																// LEFT
																index = verts.Count;
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing, windowextrude));
																uv.Add (new Vector2 ((FinalX - windowextrude) / addHeight, (H + GroundDoorBottomSpacing) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing, 0));
								
																uv.Add (new Vector2 (FinalX / addHeight, (H + GroundDoorBottomSpacing) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing + GroundDoorHeight, windowextrude));
								
																uv.Add (new Vector2 ((FinalX - windowextrude) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing + GroundDoorHeight, 0));
								
																uv.Add (new Vector2 (FinalX / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
																//FinalX = StartX + WindowWidth;					
								
								
																MeshList [LastWall_SMID].tris.Add (index + 0);
																MeshList [LastWall_SMID].tris.Add (index + 1);	
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
																// RIGHT
								
																index = verts.Count;
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing, windowextrude));
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth + windowextrude) / addHeight, (H + GroundDoorBottomSpacing) / addHeight));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing, 0));
								
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + GroundDoorBottomSpacing) / addHeight));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing + GroundDoorHeight, windowextrude));
								
																uv.Add (new Vector2 (((FinalX + GroundDoorWidth + windowextrude)) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing + GroundDoorHeight, 0));
								
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
																//FinalX = StartX + WindowWidth;								
								
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 2);	
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 0);
																// TOP
																index = verts.Count;
								
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing + GroundDoorHeight, 0));
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing + GroundDoorHeight, 0));							
																uv.Add (new Vector2 ((FinalX) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing + GroundDoorHeight, windowextrude));							
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight - windowextrude) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing + GroundDoorHeight, windowextrude));							
																uv.Add (new Vector2 ((FinalX) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight - windowextrude) / addHeight));
								
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 2);	
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 0);
																// BOTTOM
								
																index = verts.Count;
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing, 0));
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, 0));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing, 0));							
																uv.Add (new Vector2 ((FinalX) / addHeight, 0));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing, windowextrude));							
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (windowextrude) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing, windowextrude));							
																uv.Add (new Vector2 ((FinalX) / addHeight, (windowextrude) / addHeight));
								
								
																MeshList [LastWall_SMID].tris.Add (index + 0);
																MeshList [LastWall_SMID].tris.Add (index + 1);	
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
								
								
																Count++;
																//StartX += WindowWidth + WindowSpacing;
								
																FinalX = XPos + GroundDoorSpacing + GroundDoorWidth;
																StartX = XPos + GroundDoorSpacing + GroundDoorWidth + WindowSpacing;
																SMID = LastWall_SMID;
								
								
														}
							if ( StartX + WindowWidth > XPos + ThisWidth)
							{
								break;
							}
														////  WALL SECTION
														/// 
														index = verts.Count;
							
														verts.Add (new Vector3 (StartX, H + StartY, 0));
														uv.Add (new Vector2 (StartX / addHeight, StartY / addHeight));
							
														verts.Add (new Vector3 (FinalX, H + StartY, 0));							
														uv.Add (new Vector2 (FinalX / addHeight, StartY / addHeight));
							
														verts.Add (new Vector3 (StartX, H + StartY + WindowHeight, 0));							
														uv.Add (new Vector2 (StartX / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX, H + StartY + WindowHeight, 0));
							
														uv.Add (new Vector2 (FinalX / addHeight, (StartY + WindowHeight) / addHeight));
														FinalX = StartX + WindowWidth;
							
							
							
														MeshList [LastWall_SMID].tris.Add (index + 0);
														MeshList [LastWall_SMID].tris.Add (index + 1);	
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 3);
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 1);
							
							
														/////  WINDOW
														index = verts.Count;
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY, windowextrude));
														uv.Add (new Vector2 (1, 0));
							
														verts.Add (new Vector3 (StartX, H + StartY, windowextrude));
							
														uv.Add (new Vector2 (0, 0));
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY + WindowHeight, windowextrude));
							
														uv.Add (new Vector2 (1, 1));
														verts.Add (new Vector3 (StartX, H + StartY + WindowHeight, windowextrude));
							
														uv.Add (new Vector2 (0, 1));
							
														if (CurrentFloor > 0) {
																if (UpperWindowMat_UseRNDAltMat) {
																		SMID = UnityEngine.Random.Range (5, 7);
																} else {
																		SMID = 5;
																}
														} else {
																if (GroundWindowMat_UseRNDAltMat) {
																		SMID = UnityEngine.Random.Range (7, 9);
																} else {
																		SMID = 7;
																}
														}
							
														MeshList [SMID].tris.Add (index + 0);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
							
							
														//  WINDOW FRAMES
							
														// LEFT
														index = verts.Count;
							
														verts.Add (new Vector3 (StartX, H + StartY, windowextrude));
														uv.Add (new Vector2 (windowextrude / addHeight, StartY / addHeight));
							
														verts.Add (new Vector3 (StartX, H + StartY, 0));
							
														uv.Add (new Vector2 (0, StartY / addHeight));
							
														verts.Add (new Vector3 (StartX, H + StartY + WindowHeight, windowextrude));
							
														uv.Add (new Vector2 (windowextrude / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (StartX, H + StartY + WindowHeight, 0));
							
														uv.Add (new Vector2 (0, (StartY + WindowHeight) / addHeight));
														FinalX = StartX + WindowWidth;					
							
							
														MeshList [LastWall_SMID].tris.Add (index + 0);
														MeshList [LastWall_SMID].tris.Add (index + 1);	
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 3);
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 1);
														// RIGHT
							
														index = verts.Count;
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY, windowextrude));
														uv.Add (new Vector2 (windowextrude / addHeight, StartY / addHeight));
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY, 0));
							
														uv.Add (new Vector2 (0, StartY / addHeight));
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY + WindowHeight, windowextrude));
							
														uv.Add (new Vector2 (windowextrude / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY + WindowHeight, 0));
							
														uv.Add (new Vector2 (0, (StartY + WindowHeight) / addHeight));
														FinalX = StartX + WindowWidth;								
							
														MeshList [LastWall_SMID].tris.Add (index + 1);
														MeshList [LastWall_SMID].tris.Add (index + 2);	
														MeshList [LastWall_SMID].tris.Add (index + 3);
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 1);
														MeshList [LastWall_SMID].tris.Add (index + 0);
														// TOP
														index = verts.Count;
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY + WindowHeight, 0));
														uv.Add (new Vector2 ((StartX + WindowWidth) / addHeight, 0));
							
														verts.Add (new Vector3 (StartX, H + StartY + WindowHeight, 0));							
														uv.Add (new Vector2 ((StartX) / addHeight, 0));
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY + WindowHeight, windowextrude));							
														uv.Add (new Vector2 ((StartX + WindowWidth) / addHeight, (windowextrude) / addHeight));
							
														verts.Add (new Vector3 (StartX, H + StartY + WindowHeight, windowextrude));							
														uv.Add (new Vector2 ((StartX) / addHeight, (windowextrude) / addHeight));
							
														FinalX = StartX + WindowWidth;								
							
														MeshList [LastWall_SMID].tris.Add (index + 1);
														MeshList [LastWall_SMID].tris.Add (index + 2);	
														MeshList [LastWall_SMID].tris.Add (index + 3);
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 1);
														MeshList [LastWall_SMID].tris.Add (index + 0);
														// BOTTOM
							
														index = verts.Count;
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY, 0));
														uv.Add (new Vector2 ((StartX + WindowWidth) / addHeight, 0));
							
														verts.Add (new Vector3 (StartX, H + StartY, 0));							
														uv.Add (new Vector2 ((StartX) / addHeight, 0));
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY, windowextrude));							
														uv.Add (new Vector2 ((StartX + WindowWidth) / addHeight, (windowextrude) / addHeight));
							
														verts.Add (new Vector3 (StartX, H + StartY, windowextrude));							
														uv.Add (new Vector2 ((StartX) / addHeight, (windowextrude) / addHeight));
							
														FinalX = StartX + WindowWidth;								
							
														MeshList [LastWall_SMID].tris.Add (index + 0);
														MeshList [LastWall_SMID].tris.Add (index + 1);	
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 3);
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 1);
							
							
														Count++;
														StartX += WindowWidth + WindowSpacing;
												}
						
						
												index = verts.Count;
												///endwall
												/// 
												verts.Add (new Vector3 (XPos + ThisWidth, H + StartY, 0));
												uv.Add (new Vector2 ((XPos + ThisWidth) / addHeight, (StartY) / addHeight));
												verts.Add (new Vector3 (FinalX, H + StartY, 0));
						
												uv.Add (new Vector2 ((FinalX) / addHeight, (StartY) / addHeight));
												verts.Add (new Vector3 (XPos + ThisWidth, H + StartY + WindowHeight, 0));
						
												uv.Add (new Vector2 ((XPos + ThisWidth) / addHeight, (StartY + WindowHeight) / addHeight));
												verts.Add (new Vector3 (FinalX + 0, H + StartY + WindowHeight, 0));
						
												uv.Add (new Vector2 ((FinalX) / addHeight, (StartY + WindowHeight) / addHeight));
						
						
												MeshList [LastWall_SMID].tris.Add (index + 0);
												MeshList [LastWall_SMID].tris.Add (index + 1);
												MeshList [LastWall_SMID].tris.Add (index + 2);
												MeshList [LastWall_SMID].tris.Add (index + 3);
												MeshList [LastWall_SMID].tris.Add (index + 2);
												MeshList [LastWall_SMID].tris.Add (index + 1);
										}
					
					
					
					
								} else {
					
										index = verts.Count;
										verts.Add (new Vector3 (XPos + ThisWidth, H, 0));
										uv.Add (new Vector2 (ThisWidth / addHeight, 0));
										verts.Add (new Vector3 (XPos + 0, H, 0));
					
										uv.Add (new Vector2 (0, 0));
										verts.Add (new Vector3 (XPos + ThisWidth, H + addHeight, 0));
					
										uv.Add (new Vector2 (ThisWidth / addHeight, 1));
										verts.Add (new Vector3 (XPos + 0, H + addHeight, 0));
					
										uv.Add (new Vector2 (0, 1));
					
					
										MeshList [SMID].tris.Add (index + 0);
										MeshList [SMID].tris.Add (index + 1);
										MeshList [SMID].tris.Add (index + 2);
										MeshList [SMID].tris.Add (index + 3);
										MeshList [SMID].tris.Add (index + 2);
										MeshList [SMID].tris.Add (index + 1);
								}
								SMID = LastWall_SMID;
								// FRONT WALL WINDOWS

						

								// RIGHT WALL
								index = verts.Count;

								verts.Add (new Vector3 (XPos + ThisWidth, H, ThisDepth));
								uv.Add (new Vector2 (ThisWidth / addHeight, 0));
								verts.Add (new Vector3 (XPos + ThisWidth, H, 0));
								uv.Add (new Vector2 (0, 0));
								verts.Add (new Vector3 (XPos + ThisWidth, H + addHeight, ThisDepth));
								uv.Add (new Vector2 (ThisWidth / addHeight, 1));
								verts.Add (new Vector3 (XPos + ThisWidth, H + addHeight, 0));
								uv.Add (new Vector2 (0, 1));
			 
								MeshList [SMID].tris.Add (index + 0);
								MeshList [SMID].tris.Add (index + 1);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 3);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 1);

								// BACK WALL
				
								LastWall_SMID = SMID;
				if ((CurrentFloor > 0 && UseUpperWindow && UpperWindowRear) || (CurrentFloor < 1 &&(( UseGroundWindow && GroundWindowRear)||( UseGroundDoor && GroundDoorRear)))) {
										{
						
						
												float WindowHeight = 0;
						float WindowWidth = 0;
						float WindowSpacing = 0;
						float WindowBottomSpacing = 0;
						
						if (CurrentFloor > 0) {
														WindowHeight = UpperWindowHeight;
														WindowWidth = UpperWindowWidth;
														WindowSpacing = UpperWindowSpacing;
							WindowBottomSpacing = UpperWindowBottomSpacing;

							if ( UseUpperWindow == false||UpperWindowRear ==false)
							{
								WindowSpacing = ThisWidth*2;
							}
												} else {
														WindowHeight = GroundWindowHeight;
														WindowWidth = GroundWindowWidth;
							WindowSpacing = GroundWindowSpacing;
							WindowBottomSpacing = GroundWindowBottomSpacing;
							if ( UseGroundWindow == false||GroundWindowRear==false)
							{
								WindowSpacing = ThisWidth*2;
							}
												}
												int Count = 1;
						
												float CurrentWidth = WindowWidth;
												float SafeWidth = WindowWidth;


												float FinalX = XPos;
						
												float DoorSpace = 0;


												if (CurrentWidth + WindowSpacing > (ThisWidth)) {
						
												} else {
														while (CurrentWidth + WindowSpacing < (ThisWidth)) {
							
																SafeWidth = CurrentWidth;
								
																CurrentWidth += WindowSpacing + WindowWidth;
																Count++;
														}
												}											
						
												float StartX = FinalX + ((float)(ThisWidth) / 2) - (SafeWidth / 2);
												float StartY = WindowBottomSpacing;
						
						
												if ((CurrentFloor == 0 && UseGroundDoor && GroundDoorRear)) {
							
														index = verts.Count;
							
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing, H, ThisDepth));
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing) / addHeight, 0));
														verts.Add (new Vector3 (FinalX + 0, H, ThisDepth));
							
														uv.Add (new Vector2 (FinalX / addHeight, 0));
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing, H + StartY, ThisDepth));
							
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing) / addHeight, StartY / addHeight));
														verts.Add (new Vector3 (FinalX + 0, H + StartY, ThisDepth));
							
														uv.Add (new Vector2 (FinalX / addHeight, StartY / addHeight));
							
							
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 0);
							
														index = verts.Count;
							
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing - DoorSpace, H + StartY + WindowHeight, ThisDepth));
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing) / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX + 0, H + StartY + WindowHeight, ThisDepth));
														uv.Add (new Vector2 (FinalX / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing - DoorSpace, H + addHeight, ThisDepth));
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing) / addHeight, 1));
							
														verts.Add (new Vector3 (FinalX + 0, H + addHeight, ThisDepth));
														uv.Add (new Vector2 (FinalX / addHeight, 1));
							
							
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 0);
							
							
							
														////////////////////////////////////////////////////////////////////////////////////////////////
							
							
							
							
														index = verts.Count;
							
														verts.Add (new Vector3 (FinalX + ThisWidth, H, ThisDepth));
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, 0));
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing + GroundDoorWidth, H, ThisDepth));
							
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing + GroundDoorWidth) / addHeight, 0));
														verts.Add (new Vector3 (FinalX + ThisWidth, H + StartY, ThisDepth));
							
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, StartY / addHeight));
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing + GroundDoorWidth, H + StartY, ThisDepth));
							
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing + GroundDoorWidth) / addHeight, StartY / addHeight));
							
							
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 0);
							
														index = verts.Count;
							
														verts.Add (new Vector3 (FinalX + ThisWidth - DoorSpace, H + StartY + WindowHeight, ThisDepth));
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing + GroundDoorWidth, H + StartY + WindowHeight, ThisDepth));
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing + GroundDoorWidth) / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX + ThisWidth - DoorSpace, H + addHeight, ThisDepth));
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, 1));
							
														verts.Add (new Vector3 (FinalX + GroundDoorSpacing + GroundDoorWidth, H + addHeight, ThisDepth));
														uv.Add (new Vector2 ((FinalX + GroundDoorSpacing + GroundDoorWidth) / addHeight, 1));
							
							
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 0);
							
							
							
							
							
							
												} else {
							
														index = verts.Count;
							
														verts.Add (new Vector3 (FinalX + ThisWidth, H, ThisDepth));
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, 0));
														verts.Add (new Vector3 (FinalX + 0, H, ThisDepth));
							
														uv.Add (new Vector2 (FinalX / addHeight, 0));
														verts.Add (new Vector3 (FinalX + ThisWidth, H + StartY, ThisDepth));
							
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, StartY / addHeight));
														verts.Add (new Vector3 (FinalX + 0, H + StartY, ThisDepth));
							
														uv.Add (new Vector2 (FinalX / addHeight, StartY / addHeight));
							
							
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 0);
							
														index = verts.Count;
							
														verts.Add (new Vector3 (FinalX + ThisWidth - DoorSpace, H + StartY + WindowHeight, ThisDepth));
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX + 0, H + StartY + WindowHeight, ThisDepth));
														uv.Add (new Vector2 (FinalX / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX + ThisWidth - DoorSpace, H + addHeight, ThisDepth));
														uv.Add (new Vector2 ((FinalX + ThisWidth) / addHeight, 1));
							
														verts.Add (new Vector3 (FinalX + 0, H + addHeight, ThisDepth));
														uv.Add (new Vector2 (FinalX / addHeight, 1));
							
							
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 0);
							
												}
						
												Count = 0;
						
												///DO WINDOWS
												/// 
												bool doorPlaced = false;
												while (StartX + WindowWidth < XPos + ThisWidth) {
							
							
														if ((CurrentFloor == 0 && UseGroundDoor && GroundDoorRear) && StartX + WindowWidth > XPos + GroundDoorSpacing && doorPlaced == false) {
								
																doorPlaced = true;
								
								
																index = verts.Count;
								
																verts.Add (new Vector3 (XPos + GroundDoorSpacing, H + StartY, ThisDepth));
																uv.Add (new Vector2 ((XPos + GroundDoorSpacing) / addHeight, StartY / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + StartY, ThisDepth));							
																uv.Add (new Vector2 (FinalX / addHeight, StartY / addHeight));
								
																verts.Add (new Vector3 (XPos + GroundDoorSpacing, H + StartY + WindowHeight, ThisDepth));							
																uv.Add (new Vector2 ((XPos + GroundDoorSpacing) / addHeight, (StartY + WindowHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + StartY + WindowHeight, ThisDepth));
								
																uv.Add (new Vector2 (FinalX / addHeight, (StartY + WindowHeight) / addHeight));
								
								
								
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 2);	
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 0);
																FinalX = XPos + GroundDoorSpacing;
								
																//////////////////////////////////////////////////////////////////////
																/// 
								
								
																index = verts.Count;
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing + GroundDoorHeight, ThisDepth));
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing + GroundDoorHeight, ThisDepth));							
																uv.Add (new Vector2 ((FinalX) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + addHeight, ThisDepth));							
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + addHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + addHeight, ThisDepth));
								
																uv.Add (new Vector2 ((FinalX) / addHeight, (H + addHeight) / addHeight));
																//FinalX = FinalX;
								
								
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 2);	
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 0);
								
																index = verts.Count;
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H, ThisDepth));
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, H / addHeight));
								
																verts.Add (new Vector3 (FinalX, H, ThisDepth));							
																uv.Add (new Vector2 ((FinalX) / addHeight, H / addHeight));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing, ThisDepth));							
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + GroundDoorBottomSpacing) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing, ThisDepth));
								
																uv.Add (new Vector2 ((FinalX) / addHeight, (H + GroundDoorBottomSpacing) / addHeight));
																//FinalX = FinalX;
								
								
								
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 2);	
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 0);
								
								
								
								
																/////  WINDOW
																index = verts.Count;
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing, ThisDepth - windowextrude));
																uv.Add (new Vector2 (0, 0));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing, ThisDepth - windowextrude));
								
																uv.Add (new Vector2 (1, 0));
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing + GroundDoorHeight, ThisDepth - windowextrude));
								
																uv.Add (new Vector2 (0, 1));
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing + GroundDoorHeight, ThisDepth - windowextrude));
								
																uv.Add (new Vector2 (1, 1));
								
																if (GroundDoorMat_UseRNDAltMat) {
																		SMID = UnityEngine.Random.Range (9, 11);
																} else {
																		SMID = 9;
																}
								
																MeshList [SMID].tris.Add (index + 1);
																MeshList [SMID].tris.Add (index + 2);
																MeshList [SMID].tris.Add (index + 3);
																MeshList [SMID].tris.Add (index + 2);
																MeshList [SMID].tris.Add (index + 1);
																MeshList [SMID].tris.Add (index + 0);
								
								
																//  WINDOW FRAMES
								
																// LEFT
																index = verts.Count;
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing, ThisDepth - windowextrude));
																uv.Add (new Vector2 ((FinalX - windowextrude) / addHeight, (H + GroundDoorBottomSpacing) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing, ThisDepth));
								
																uv.Add (new Vector2 (FinalX / addHeight, (H + GroundDoorBottomSpacing) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing + GroundDoorHeight, ThisDepth - windowextrude));
								
																uv.Add (new Vector2 ((FinalX - windowextrude) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing + GroundDoorHeight, ThisDepth));
								
																uv.Add (new Vector2 (FinalX / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
																//FinalX = StartX + WindowWidth;					
								
								
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 2);	
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 0);
																// RIGHT
								
																index = verts.Count;
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing, ThisDepth - windowextrude));
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth + windowextrude) / addHeight, (H + GroundDoorBottomSpacing) / addHeight));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing, ThisDepth));
								
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + GroundDoorBottomSpacing) / addHeight));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing + GroundDoorHeight, ThisDepth - windowextrude));
								
																uv.Add (new Vector2 (((FinalX + GroundDoorWidth + windowextrude)) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing + GroundDoorHeight, ThisDepth));
								
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
																//FinalX = StartX + WindowWidth;								
								
																MeshList [LastWall_SMID].tris.Add (index + 0);
																MeshList [LastWall_SMID].tris.Add (index + 1);	
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
																// TOP
																index = verts.Count;
								
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing + GroundDoorHeight, ThisDepth));
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing + GroundDoorHeight, ThisDepth));							
																uv.Add (new Vector2 ((FinalX) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight) / addHeight));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing + GroundDoorHeight, ThisDepth - windowextrude));							
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight - windowextrude) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing + GroundDoorHeight, ThisDepth - windowextrude));							
																uv.Add (new Vector2 ((FinalX) / addHeight, (H + GroundDoorBottomSpacing + GroundDoorHeight - windowextrude) / addHeight));
								
																MeshList [LastWall_SMID].tris.Add (index + 0);
																MeshList [LastWall_SMID].tris.Add (index + 1);	
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
																// BOTTOM
								
																index = verts.Count;
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing, ThisDepth));
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, 0));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing, ThisDepth));							
																uv.Add (new Vector2 ((FinalX) / addHeight, 0));
								
																verts.Add (new Vector3 (FinalX + GroundDoorWidth, H + GroundDoorBottomSpacing, ThisDepth - windowextrude));							
																uv.Add (new Vector2 ((FinalX + GroundDoorWidth) / addHeight, (windowextrude) / addHeight));
								
																verts.Add (new Vector3 (FinalX, H + GroundDoorBottomSpacing, ThisDepth - windowextrude));							
																uv.Add (new Vector2 ((FinalX) / addHeight, (windowextrude) / addHeight));
								
								
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 2);	
																MeshList [LastWall_SMID].tris.Add (index + 3);
																MeshList [LastWall_SMID].tris.Add (index + 2);
																MeshList [LastWall_SMID].tris.Add (index + 1);
																MeshList [LastWall_SMID].tris.Add (index + 0);
								
								
																Count++;
																//StartX += WindowWidth + WindowSpacing;
								
																FinalX = XPos + GroundDoorSpacing + GroundDoorWidth;
																StartX = XPos + GroundDoorSpacing + GroundDoorWidth + WindowWidth;
																SMID = LastWall_SMID;
								
								
														}
							if ( StartX + WindowWidth > XPos + ThisWidth|| (WindowSpacing>ThisWidth))
							{
								break;
							}
														////  WALL SECTION
														/// 
														index = verts.Count;
							
														verts.Add (new Vector3 (StartX, H + StartY, ThisDepth));
														uv.Add (new Vector2 (StartX / addHeight, StartY / addHeight));
							
														verts.Add (new Vector3 (FinalX, H + StartY, ThisDepth));							
														uv.Add (new Vector2 (FinalX / addHeight, StartY / addHeight));
							
														verts.Add (new Vector3 (StartX, H + StartY + WindowHeight, ThisDepth));							
														uv.Add (new Vector2 (StartX / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (FinalX, H + StartY + WindowHeight, ThisDepth));
							
														uv.Add (new Vector2 (FinalX / addHeight, (StartY + WindowHeight) / addHeight));
														FinalX = StartX + WindowWidth;
							
							
							
														MeshList [LastWall_SMID].tris.Add (index + 1);
														MeshList [LastWall_SMID].tris.Add (index + 2);	
														MeshList [LastWall_SMID].tris.Add (index + 3);
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 1);
														MeshList [LastWall_SMID].tris.Add (index + 0);
							
							
														/////  WINDOW
														index = verts.Count;
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY, ThisDepth - windowextrude));
														uv.Add (new Vector2 (0, 0));
							
														verts.Add (new Vector3 (StartX, H + StartY, ThisDepth - windowextrude));
							
														uv.Add (new Vector2 (1, 0));
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY + WindowHeight, ThisDepth - windowextrude));
							
														uv.Add (new Vector2 (0, 1));
														verts.Add (new Vector3 (StartX, H + StartY + WindowHeight, ThisDepth - windowextrude));
							
														uv.Add (new Vector2 (1, 1));
							
														if (CurrentFloor > 0) {
																if (UpperWindowMat_UseRNDAltMat) {
																		SMID = UnityEngine.Random.Range (5, 7);
																} else {
																		SMID = 5;
																}
														} else {
																if (GroundWindowMat_UseRNDAltMat) {
																		SMID = UnityEngine.Random.Range (7, 9);
																} else {
																		SMID = 7;
																}
														}
							
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 3);
														MeshList [SMID].tris.Add (index + 2);
														MeshList [SMID].tris.Add (index + 1);
														MeshList [SMID].tris.Add (index + 0);
							
							
														//  WINDOW FRAMES
							
														// LEFT
														index = verts.Count;
							
														verts.Add (new Vector3 (StartX, H + StartY, ThisDepth - windowextrude));
														uv.Add (new Vector2 (windowextrude / addHeight, StartY / addHeight));
							
														verts.Add (new Vector3 (StartX, H + StartY, ThisDepth));
							
														uv.Add (new Vector2 (0, StartY / addHeight));
							
														verts.Add (new Vector3 (StartX, H + StartY + WindowHeight, ThisDepth - windowextrude));
							
														uv.Add (new Vector2 (windowextrude / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (StartX, H + StartY + WindowHeight, ThisDepth));
							
														uv.Add (new Vector2 (0, (StartY + WindowHeight) / addHeight));
														FinalX = StartX + WindowWidth;					
							
							
														MeshList [LastWall_SMID].tris.Add (index + 1);
														MeshList [LastWall_SMID].tris.Add (index + 2);	
														MeshList [LastWall_SMID].tris.Add (index + 3);
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 1);
														MeshList [LastWall_SMID].tris.Add (index + 0);
														// RIGHT
							
														index = verts.Count;
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY, ThisDepth - windowextrude));
														uv.Add (new Vector2 (windowextrude / addHeight, StartY / addHeight));
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY, ThisDepth));
							
														uv.Add (new Vector2 (0, StartY / addHeight));
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY + WindowHeight, ThisDepth - windowextrude));
							
														uv.Add (new Vector2 (windowextrude / addHeight, (StartY + WindowHeight) / addHeight));
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY + WindowHeight, ThisDepth));
							
														uv.Add (new Vector2 (0, (StartY + WindowHeight) / addHeight));
														FinalX = StartX + WindowWidth;								
							
														MeshList [LastWall_SMID].tris.Add (index + 0);
														MeshList [LastWall_SMID].tris.Add (index + 1);	
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 3);
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 1);
														// TOP
														index = verts.Count;
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY + WindowHeight, ThisDepth));
														uv.Add (new Vector2 ((StartX + WindowWidth) / addHeight, 0));
							
														verts.Add (new Vector3 (StartX, H + StartY + WindowHeight, ThisDepth));							
														uv.Add (new Vector2 ((StartX) / addHeight, 0));
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY + WindowHeight, ThisDepth - windowextrude));							
														uv.Add (new Vector2 ((StartX + WindowWidth) / addHeight, (windowextrude) / addHeight));
							
														verts.Add (new Vector3 (StartX, H + StartY + WindowHeight, ThisDepth - windowextrude));							
														uv.Add (new Vector2 ((StartX) / addHeight, (windowextrude) / addHeight));
							
														FinalX = StartX + WindowWidth;								
							
														MeshList [LastWall_SMID].tris.Add (index + 0);
														MeshList [LastWall_SMID].tris.Add (index + 1);	
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 3);
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 1);
														// BOTTOM
							
														index = verts.Count;
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY, ThisDepth));
														uv.Add (new Vector2 ((StartX + WindowWidth) / addHeight, 0));
							
														verts.Add (new Vector3 (StartX, H + StartY, ThisDepth));							
														uv.Add (new Vector2 ((StartX) / addHeight, 0));
							
														verts.Add (new Vector3 (StartX + WindowWidth, H + StartY, ThisDepth - windowextrude));							
														uv.Add (new Vector2 ((StartX + WindowWidth) / addHeight, (windowextrude) / addHeight));
							
														verts.Add (new Vector3 (StartX, H + StartY, ThisDepth - windowextrude));							
														uv.Add (new Vector2 ((StartX) / addHeight, (windowextrude) / addHeight));
							
														FinalX = StartX + WindowWidth;								
							
														MeshList [LastWall_SMID].tris.Add (index + 1);
														MeshList [LastWall_SMID].tris.Add (index + 2);	
														MeshList [LastWall_SMID].tris.Add (index + 3);
														MeshList [LastWall_SMID].tris.Add (index + 2);
														MeshList [LastWall_SMID].tris.Add (index + 1);
														MeshList [LastWall_SMID].tris.Add (index + 0);
							
							
														Count++;
														StartX += WindowWidth + WindowSpacing;
												}
						
						
												index = verts.Count;
												///endwall
												/// 
												verts.Add (new Vector3 (XPos + ThisWidth, H + StartY, ThisDepth));
												uv.Add (new Vector2 ((XPos + ThisWidth) / addHeight, (StartY) / addHeight));
												verts.Add (new Vector3 (FinalX, H + StartY, ThisDepth));
						
												uv.Add (new Vector2 ((FinalX) / addHeight, (StartY) / addHeight));
												verts.Add (new Vector3 (XPos + ThisWidth, H + StartY + WindowHeight, ThisDepth));
						
												uv.Add (new Vector2 ((XPos + ThisWidth) / addHeight, (StartY + WindowHeight) / addHeight));
												verts.Add (new Vector3 (FinalX + 0, H + StartY + WindowHeight, ThisDepth));
						
												uv.Add (new Vector2 ((FinalX) / addHeight, (StartY + WindowHeight) / addHeight));
						
						
												MeshList [LastWall_SMID].tris.Add (index + 1);
												MeshList [LastWall_SMID].tris.Add (index + 2);
												MeshList [LastWall_SMID].tris.Add (index + 3);
												MeshList [LastWall_SMID].tris.Add (index + 2);
												MeshList [LastWall_SMID].tris.Add (index + 1);
												MeshList [LastWall_SMID].tris.Add (index + 0);
										}
					
					
					
					
								} else {
					
										index = verts.Count;
										verts.Add (new Vector3 (XPos + ThisWidth, H, ThisDepth));
										uv.Add (new Vector2 (ThisWidth / addHeight, 0));
										verts.Add (new Vector3 (XPos + 0, H, ThisDepth));
					
										uv.Add (new Vector2 (0, 0));
										verts.Add (new Vector3 (XPos + ThisWidth, H + addHeight, ThisDepth));
					
										uv.Add (new Vector2 (ThisWidth / addHeight, 1));
										verts.Add (new Vector3 (XPos + 0, H + addHeight, ThisDepth));
					
										uv.Add (new Vector2 (0, 1));
					
					
										MeshList [SMID].tris.Add (index + 1);
										MeshList [SMID].tris.Add (index + 2);
										MeshList [SMID].tris.Add (index + 3);
										MeshList [SMID].tris.Add (index + 2);
										MeshList [SMID].tris.Add (index + 1);
										MeshList [SMID].tris.Add (index + 0);
								}
								SMID = LastWall_SMID;
			
								// RIGHT WALL
								index = verts.Count;
			
								verts.Add (new Vector3 (XPos + 0, H, 0));
								uv.Add (new Vector2 (ThisDepth / addHeight, 0));
								verts.Add (new Vector3 (XPos + 0, H, ThisDepth));
								uv.Add (new Vector2 (0, 0));
								verts.Add (new Vector3 (XPos + 0, H + addHeight, 0));
								uv.Add (new Vector2 (ThisDepth / addHeight, 1));
								verts.Add (new Vector3 (XPos + 0, H + addHeight, ThisDepth));
								uv.Add (new Vector2 (0, 1));
			
								MeshList [SMID].tris.Add (index + 0);
								MeshList [SMID].tris.Add (index + 1);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 3);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 1);

								LastWall_SMID = SMID;

								

				
								if (CurrentFloor < 1) {
										H += GF_Height;
								} else {
										H += SF_Height;
								}
								CurrentFloor++;
						}

		
						//ROOF
						SMID = LastWall_SMID;
						if (RT == RoofTypes.Flat) {

								if (RoofMat_UseRNDAltMat) {
										SMID = UnityEngine.Random.Range (3, 5);
								} else {
										SMID = 3;
								}
								index = verts.Count;
			
								verts.Add (new Vector3 (XPos - RoofOverhang, H + Roof_Height, 0 - RoofFrontOverhang));			
								uv.Add (new Vector2 (0, 0));
								verts.Add (new Vector3 (XPos - RoofOverhang, H + Roof_Height, ThisDepth + RoofOverhang));
								uv.Add (new Vector2 (0, 1));
								verts.Add (new Vector3 (XPos + ThisWidth + RoofOverhang, H + Roof_Height, 0 - RoofFrontOverhang));
								uv.Add (new Vector2 (1, 0));
								verts.Add (new Vector3 (XPos + ThisWidth + RoofOverhang, H + Roof_Height, ThisDepth + RoofOverhang));
			
								uv.Add (new Vector2 (1, 1));
			
								MeshList [SMID].tris.Add (index + 0);
								MeshList [SMID].tris.Add (index + 1);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 3);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 1);
				
								////////////////////
								index = verts.Count;
				
								verts.Add (new Vector3 (XPos + ThisWidth + RoofOverhang, H, 0 - RoofFrontOverhang));			
								uv.Add (new Vector2 ((XPos + ThisWidth + RoofOverhang) / addHeight, H / addHeight));
								verts.Add (new Vector3 (XPos - RoofOverhang, H, 0 - RoofFrontOverhang));
								uv.Add (new Vector2 ((XPos - RoofOverhang) / addHeight, H / addHeight));
								verts.Add (new Vector3 (XPos + ThisWidth + RoofOverhang, H + Roof_Height, 0 - RoofFrontOverhang));
								uv.Add (new Vector2 ((XPos + ThisWidth + RoofOverhang) / addHeight, (H + Roof_Height) / addHeight));
								verts.Add (new Vector3 (XPos - RoofOverhang, H + Roof_Height, 0 - RoofFrontOverhang));
				
								uv.Add (new Vector2 ((XPos - RoofOverhang) / addHeight, (H + Roof_Height) / addHeight));
				
								MeshList [SMID].tris.Add (index + 0);
								MeshList [SMID].tris.Add (index + 1);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 3);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 1);
				
				
								////////////////////
								index = verts.Count;
				
								verts.Add (new Vector3 (XPos + ThisWidth + RoofOverhang, H, 0 - RoofFrontOverhang));			
								uv.Add (new Vector2 ((0 - RoofFrontOverhang) / addHeight, H / addHeight));
				
								verts.Add (new Vector3 (XPos + ThisWidth + RoofOverhang, H, ThisDepth + RoofOverhang));
								uv.Add (new Vector2 ((ThisDepth + RoofOverhang) / addHeight, H / addHeight));
				
								verts.Add (new Vector3 (XPos + ThisWidth + RoofOverhang, H + Roof_Height, 0 - RoofFrontOverhang));
								uv.Add (new Vector2 ((0 - RoofFrontOverhang) / addHeight, (H + Roof_Height) / addHeight));
				
								verts.Add (new Vector3 (XPos + ThisWidth + RoofOverhang, H + Roof_Height, ThisDepth + RoofOverhang));				
								uv.Add (new Vector2 ((ThisDepth + RoofOverhang) / addHeight, (H + Roof_Height) / addHeight));
				
								MeshList [SMID].tris.Add (index + 1);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 3);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 1);
								MeshList [SMID].tris.Add (index + 0);
				
								////////////////////
								index = verts.Count;
				
								verts.Add (new Vector3 (XPos - RoofOverhang, H, 0 - RoofFrontOverhang));			
								uv.Add (new Vector2 ((0 - RoofFrontOverhang) / addHeight, H / addHeight));
				
								verts.Add (new Vector3 (XPos - RoofOverhang, H, ThisDepth + RoofOverhang));
								uv.Add (new Vector2 ((ThisDepth + RoofOverhang) / addHeight, H / addHeight));
				
								verts.Add (new Vector3 (XPos - RoofOverhang, H + Roof_Height, 0 - RoofFrontOverhang));
								uv.Add (new Vector2 ((0 - RoofFrontOverhang) / addHeight, (H + Roof_Height) / addHeight));
				
								verts.Add (new Vector3 (XPos - RoofOverhang, H + Roof_Height, ThisDepth + RoofOverhang));				
								uv.Add (new Vector2 ((ThisDepth + RoofOverhang) / addHeight, (H + Roof_Height) / addHeight));
				
								MeshList [SMID].tris.Add (index + 0);
								MeshList [SMID].tris.Add (index + 1);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 3);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 1);


								////////////////////
								index = verts.Count;
				
								verts.Add (new Vector3 (XPos + ThisWidth + RoofOverhang, H, ThisDepth + RoofOverhang));			
								uv.Add (new Vector2 ((XPos + ThisWidth + RoofOverhang) / addHeight, H / addHeight));
								verts.Add (new Vector3 (XPos - RoofOverhang, H, ThisDepth + RoofOverhang));
								uv.Add (new Vector2 ((XPos - RoofOverhang) / addHeight, H / addHeight));
								verts.Add (new Vector3 (XPos + ThisWidth + RoofOverhang, H + Roof_Height, ThisDepth + RoofOverhang));
								uv.Add (new Vector2 ((XPos + ThisWidth + RoofOverhang) / addHeight, (H + Roof_Height) / addHeight));
								verts.Add (new Vector3 (XPos - RoofOverhang, H + Roof_Height, ThisDepth + RoofOverhang));
				
								uv.Add (new Vector2 ((XPos - RoofOverhang) / addHeight, (H + Roof_Height) / addHeight));
				
								MeshList [SMID].tris.Add (index + 1);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 3);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 1);
								MeshList [SMID].tris.Add (index + 0);
				
								////////////////////


				
								index = verts.Count;
				
								verts.Add (new Vector3 (XPos - RoofOverhang, H, 0 - RoofFrontOverhang));
				
								uv.Add (new Vector2 (0, 0));
								verts.Add (new Vector3 (XPos - RoofOverhang, H, ThisDepth + RoofOverhang));
								uv.Add (new Vector2 (0, 1));
								verts.Add (new Vector3 (XPos + ThisWidth + RoofOverhang, H, 0 - RoofFrontOverhang));
								uv.Add (new Vector2 (1, 0));
								verts.Add (new Vector3 (XPos + ThisWidth + RoofOverhang, H, ThisDepth + RoofOverhang));
				
								uv.Add (new Vector2 (1, 1));
				
								MeshList [SMID].tris.Add (index + 1);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 3);
								MeshList [SMID].tris.Add (index + 2);
								MeshList [SMID].tris.Add (index + 1);
								MeshList [SMID].tris.Add (index + 0);


			} else if (RT == RoofTypes.Gable) {
				
				/// FRONT TRIANGLE
				
				
				index = verts.Count;
				
				verts.Add (new Vector3 (XPos + ThisWidth, H, 0));
				
				uv.Add (new Vector2 ((float)(ThisWidth) / addHeight, 0));
				verts.Add (new Vector3 (XPos + 0, H, 0));
				
				uv.Add (new Vector2 (0, 0));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0));
				
				uv.Add (new Vector2 (((float)(ThisWidth) / addHeight) / 2, Roof_Height / addHeight));
				
				MeshList [SMID].tris.Add (index + 0);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				
				
				/// BACK TRIANGLE
				
				
				
				index = verts.Count;
				
				verts.Add (new Vector3 (XPos + 0, H, ThisDepth));
				uv.Add (new Vector2 ((float)(ThisWidth) / addHeight, 0));
				verts.Add (new Vector3 (XPos + ThisWidth, H, ThisDepth));
				uv.Add (new Vector2 (0, 0));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth));
				uv.Add (new Vector2 (((float)(ThisWidth) / addHeight) / 2, Roof_Height / addHeight));
				
				MeshList [SMID].tris.Add (index + 0);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				
				
				
				//SlopeRoof
				
				if (RoofMat_UseRNDAltMat) {
					SMID = UnityEngine.Random.Range (3, 5);
				} else {
					SMID = 3;
				}
				index = verts.Count;
				
				verts.Add ((new Vector3 (XPos + 0, H + 0.05f, 0 - RoofFrontOverhang)) + ((new Vector3 (XPos + 0, H + 0.05f, 0 - RoofFrontOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0- RoofFrontOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0));
				verts.Add ((new Vector3 (XPos + 0, H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + 0, H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (0, 0));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0 - RoofFrontOverhang));
				uv.Add (new Vector2 (1, 1));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang));
				uv.Add (new Vector2 (0, 1));
				MeshList [SMID].tris.Add (index + 0);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				
				index = verts.Count;
				
				verts.Add ((new Vector3 (XPos + 0, H, 0 - RoofFrontOverhang)) + ((new Vector3 (XPos + 0, H, 0 - RoofFrontOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0 - RoofFrontOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0));
				verts.Add ((new Vector3 (XPos + 0, H, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + 0, H, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (0, 0));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0 - RoofFrontOverhang));
				uv.Add (new Vector2 (1, 1));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth + RoofOverhang));
				uv.Add (new Vector2 (0, 1));
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 0);
				
				index = verts.Count;
				
				verts.Add ((new Vector3 (XPos + 0, H , 0 - RoofFrontOverhang)) + ((new Vector3 (XPos + 0, H , 0 - RoofFrontOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H  + Roof_Height, 0- RoofFrontOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0));
				verts.Add ((new Vector3 (XPos + 0, H , ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + 0, H , ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H  + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (0, 0));
				verts.Add ((new Vector3 (XPos + 0, H + 0.05f, 0 - RoofFrontOverhang)) + ((new Vector3 (XPos + 0, H + 0.05f, 0 - RoofFrontOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0- RoofFrontOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0.1f));
				verts.Add ((new Vector3 (XPos + 0, H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + 0, H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (0, 0.1f));
				
				MeshList [SMID].tris.Add (index + 0);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				
				
				///////////		
				
				index = verts.Count;		
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofFrontOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofFrontOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0 - RoofFrontOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0));
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth+ RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (0, 0));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0 - RoofFrontOverhang));
				uv.Add (new Vector2 (1, 1));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang));
				uv.Add (new Vector2 (0, 1));
				
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 0);
				index = verts.Count;		
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H, 0 - RoofFrontOverhang)) + ((new Vector3 (XPos + ThisWidth, H, 0- RoofFrontOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0- RoofFrontOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0));
				verts.Add ((new Vector3 (XPos + ThisWidth, H, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth+ RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (0, 0));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0 - RoofFrontOverhang));
				uv.Add (new Vector2 (1, 1));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth + RoofOverhang));
				uv.Add (new Vector2 (0, 1));
				
				MeshList [SMID].tris.Add (index + 0);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				
				
				index = verts.Count;		
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H , 0 - RoofFrontOverhang)) + ((new Vector3 (XPos + ThisWidth, H , 0 - RoofFrontOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0 - RoofFrontOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0));
				verts.Add ((new Vector3 (XPos + ThisWidth, H , ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H , ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H  + Roof_Height, ThisDepth+ RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (0, 0));
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofFrontOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofFrontOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0 - RoofFrontOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0.1f));
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth+ RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (0, 0.1f));
				
				
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 0);
				/////////
				/// 
				
				
				
				index = verts.Count;		
				
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0 - RoofFrontOverhang));
				uv.Add (new Vector2 (0, 0));
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H, 0 - RoofFrontOverhang)) + ((new Vector3 (XPos + ThisWidth, H, 0 - RoofFrontOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0 - RoofFrontOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0));
				
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0 - RoofFrontOverhang));
				uv.Add (new Vector2 (1, 0.1f));
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofFrontOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofFrontOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0 - RoofFrontOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0.1f));
				
				
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 0);
				/// 
				
				
				
				index = verts.Count;		
				
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth + RoofOverhang));
				uv.Add (new Vector2 (0, 0));
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0));
				
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang));
				uv.Add (new Vector2 (1, 0.1f));
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0.1f));
				
				
				MeshList [SMID].tris.Add (index + 0);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				
				/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
				
				
				index = verts.Count;		
				
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0 - RoofFrontOverhang));
				uv.Add (new Vector2 (0, 0));
				
				verts.Add ((new Vector3 (XPos , H, 0 - RoofFrontOverhang)) + ((new Vector3 (XPos , H, 0 - RoofFrontOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0 - RoofFrontOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0));
				
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0 - RoofFrontOverhang));
				uv.Add (new Vector2 (1, 0.1f));
				
				verts.Add ((new Vector3 (XPos , H + 0.05f, 0 - RoofFrontOverhang)) + ((new Vector3 (XPos , H + 0.05f, 0 - RoofFrontOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0 - RoofFrontOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0.1f));
				
				
				MeshList [SMID].tris.Add (index + 0);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				/// 
				
				
				
				index = verts.Count;		
				
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth + RoofOverhang));
				uv.Add (new Vector2 (0, 0));
				
				verts.Add ((new Vector3 (XPos , H, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos , H, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0));
				
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang));
				uv.Add (new Vector2 (1, 0.1f));
				
				verts.Add ((new Vector3 (XPos , H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos , H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0.1f));
				
				
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 0);
				
			} else if (RT == RoofTypes.Hip) {
				
				//SlopeRoof
				
				if (RoofMat_UseRNDAltMat) {
					SMID = UnityEngine.Random.Range (3, 5);
				} else {
					SMID = 3;
				}
				index = verts.Count;
				
				verts.Add ((new Vector3 (XPos + 0, H + 0.05f, 0 - RoofOverhang)) + ((new Vector3 (XPos + 0, H + 0.05f, 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0- RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((0 - RoofOverhang)/ThisDepth, 0));
				verts.Add ((new Vector3 (XPos + 0, H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + 0, H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisDepth + RoofOverhang)/ThisDepth, 0));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ((ThisDepth / 2) - ( HipSize/2) )));
				uv.Add (new Vector2 ((((ThisDepth / 2) - ( HipSize/2))/ThisDepth), 1));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ((ThisDepth / 2) + ( HipSize/2) )));
				uv.Add (new Vector2 ((((ThisDepth / 2) + ( HipSize/2))/ThisDepth), 1));
				MeshList [SMID].tris.Add (index + 0);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				
				index = verts.Count;
				
				verts.Add ((new Vector3 (XPos + 0, H, 0 - RoofOverhang)) + ((new Vector3 (XPos + 0, H, 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0 - RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((0- RoofOverhang)/ThisDepth, 0));
				verts.Add ((new Vector3 (XPos + 0, H, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + 0, H, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisDepth + RoofOverhang)/ThisDepth, 0));
				verts.Add (new Vector3(XPos ,H,0));
				uv.Add (new Vector2 (0, 1));
				verts.Add (new Vector3(XPos ,H,ThisDepth));
				uv.Add (new Vector2 (1, 1));

				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 0);
				
				index = verts.Count;
				
				verts.Add ((new Vector3 (XPos + 0, H , 0 - RoofOverhang)) + ((new Vector3 (XPos + 0, H , 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H  + Roof_Height, 0- RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0));
				verts.Add ((new Vector3 (XPos + 0, H , ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + 0, H , ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H  + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (0, 0));
				verts.Add ((new Vector3 (XPos + 0, H + 0.05f, 0 - RoofOverhang)) + ((new Vector3 (XPos + 0, H + 0.05f, 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0- RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0.1f));
				verts.Add ((new Vector3 (XPos + 0, H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + 0, H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (0, 0.1f));
				
				MeshList [SMID].tris.Add (index + 0);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				
				
				///////////		
				
				index = verts.Count;		
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0 - RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((0- RoofOverhang)/ThisDepth, 0));
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth+ RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisDepth + RoofOverhang)/ThisDepth, 0));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ((ThisDepth / 2) - ( HipSize/2) )));
				uv.Add (new Vector2 ((((ThisDepth / 2) - ( HipSize/2))/ThisDepth), 1));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ((ThisDepth / 2) + ( HipSize/2) )));
				uv.Add (new Vector2 ((((ThisDepth / 2) + ( HipSize/2))/ThisDepth), 1));
				
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 0);
				index = verts.Count;		
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H, 0 - RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H, 0- RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0- RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((0- RoofOverhang)/ThisDepth, 0));
				verts.Add ((new Vector3 (XPos + ThisWidth, H, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth+ RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisDepth + RoofOverhang)/ThisDepth, 0));


				verts.Add (new Vector3(XPos +ThisWidth,H,0));
				uv.Add (new Vector2 (0, 1));
				           verts.Add (new Vector3(XPos +ThisWidth,H,ThisDepth));
				uv.Add (new Vector2 (1, 1));
				
				MeshList [SMID].tris.Add (index + 0);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				
				
				index = verts.Count;		
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H , 0 - RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H , 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0 - RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0));
				verts.Add ((new Vector3 (XPos + ThisWidth, H , ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H , ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H  + Roof_Height, ThisDepth+ RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (0, 0));
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0 - RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (1, 0.1f));
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth+ RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 (0, 0.1f));
				
				
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 0);
				////////////////////////////////////////////////////////////////////////
				/// FRONT SLOPE
				
				index = verts.Count;		
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0 - RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisWidth+ RoofOverhang)/ThisWidth, 0));
				verts.Add ((new Vector3 (XPos + 0, H + 0.05f, 0 - RoofOverhang)) + ((new Vector3 (XPos + 0, H + 0.05f, 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0- RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((0 - RoofOverhang)/ThisWidth, 0));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H+ 0.05f + Roof_Height, ((ThisDepth / 2) - ( HipSize /2) )));
				uv.Add (new Vector2 (((float)((float)(ThisWidth) / 2))/ThisWidth, 1));
				
				MeshList [SMID].tris.Add (index + 0);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				index = verts.Count;		
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H, 0 - RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H, 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0 - RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisWidth+ RoofOverhang)/ThisWidth, 0));
				verts.Add ((new Vector3 (XPos + 0, H, 0 - RoofOverhang)) + ((new Vector3 (XPos + 0, H, 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0- RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((0 - RoofOverhang)/ThisWidth, 0));
				
				
				verts.Add (new Vector3(XPos+ThisWidth,H,0));
				uv.Add (new Vector2(1,0.5f));
				verts.Add (new Vector3(XPos+0,H,0));
				uv.Add (new Vector2(0,0.5f));
				
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 0);
				
				index = verts.Count;		
				verts.Add ((new Vector3 (XPos + ThisWidth, H, 0 - RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H, 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0 - RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisWidth+ RoofOverhang)/ThisWidth, 0));
				verts.Add ((new Vector3 (XPos + 0, H, 0 - RoofOverhang)) + ((new Vector3 (XPos + 0, H, 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, 0- RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((0 - RoofOverhang)/ThisWidth, 0));
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0 - RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisWidth+ RoofOverhang)/ThisWidth, 0));
				verts.Add ((new Vector3 (XPos + 0, H + 0.05f, 0 - RoofOverhang)) + ((new Vector3 (XPos + 0, H + 0.05f, 0 - RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, 0- RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((0 - RoofOverhang)/ThisWidth, 0));
				
				MeshList [SMID].tris.Add (index + 0);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				
				//////////////////////////////////////////////
				////////////////////////////////////////////////////////////////////////
				/// BACK SLOPE
				
				index = verts.Count;		
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisWidth+ RoofOverhang)/ThisWidth, 0));
				verts.Add ((new Vector3 (XPos + 0, H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + 0, H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((0- RoofOverhang)/ThisWidth, 0));
				verts.Add (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H+ 0.05f + Roof_Height, ((ThisDepth / 2) + ( HipSize /2) )));
				uv.Add (new Vector2 (((float)((float)(ThisWidth) / 2))/ThisWidth, 1));
				
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 0);
				index = verts.Count;		
				
				verts.Add ((new Vector3 (XPos + ThisWidth, H, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisWidth + RoofOverhang) / ThisWidth, 0));
				verts.Add ((new Vector3 (XPos + 0, H, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + 0, H, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((0 - RoofOverhang) / ThisWidth, 0));
				
				
				verts.Add (new Vector3(XPos+ThisWidth,H,ThisDepth ));
				uv.Add (new Vector2(1,0.5f));
				verts.Add (new Vector3(XPos+0,H,ThisDepth ));
				uv.Add (new Vector2(0,0.5f));
				
				MeshList [SMID].tris.Add (index + 0);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				
				index = verts.Count;		
				verts.Add ((new Vector3 (XPos + ThisWidth, H, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisWidth+ RoofOverhang)/ThisWidth, 0));
				verts.Add ((new Vector3 (XPos + 0, H, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + 0, H, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisDepth + RoofOverhang)/ThisWidth, 0));
				verts.Add ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + ThisWidth, H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisWidth+ RoofOverhang)/ThisWidth, 0.5f));
				verts.Add ((new Vector3 (XPos + 0, H + 0.05f, ThisDepth + RoofOverhang)) + ((new Vector3 (XPos + 0, H + 0.05f, ThisDepth + RoofOverhang)) - (new Vector3 (XPos + (float)((float)(ThisWidth) / 2), H + 0.05f + Roof_Height, ThisDepth + RoofOverhang))).normalized * RoofOverhang);
				uv.Add (new Vector2 ((ThisDepth + RoofOverhang)/ThisWidth, 0.5f));
				
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 3);
				MeshList [SMID].tris.Add (index + 2);
				MeshList [SMID].tris.Add (index + 1);
				MeshList [SMID].tris.Add (index + 0);
				
				//////////////////////////////////////////////
				
			}
						XPos += ThisWidth;
						BuildingNumber++;
				}
				mesh.subMeshCount = MeshList.Count;
				int SM_C = 0;
				mesh.vertices = verts.ToArray ();
				mesh.uv = uv.ToArray ();
				renderer.sharedMaterials = MatList.ToArray ();
				foreach (SubMesh SM in MeshList) {

			
						//mesh.SetIndices ( SM.tris.ToArray () , MeshTopology.Triangles, SM_C);
						mesh.SetTriangles (SM.tris.ToArray (), SM_C);
			
						//mesh.u
						SM_C++;
				}
				mesh.Optimize ();
				mesh.RecalculateNormals ();
				GetComponent<MeshCollider> ().sharedMesh = mesh;
		}




}
