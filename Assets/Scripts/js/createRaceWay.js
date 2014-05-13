// Monotone chain convex hull : 
// Converted to Unity by - mgear - http://unitycoder.com/blog

#pragma downcast

var homogenizePointsDistanceThreshhold : int = 40;
var maxpoints : int = 20;
var samples : int = 7;
private var points:Vector3[] = new Vector3[maxpoints];
private var PointsInConvexHull:Vector3[];
private var curvedFrame:Vector3[];
private var newPointsInConvexHull:Vector3[];
var WayPoints: Vector3[];
private var CleanedUpPoints : Vector3[];
private var SecondCleanUpPoints : Vector3[];
var TrackScaler : float = 17.0f;
private var ExteriorVertices : Vector3[];
private var InteriorVertices : Vector3[];
var NumberOfOpponents : int = 8;

var RoadMaterial : Material;
var WallMaterial : Material;

var PolePositions : Transform[] = new Transform[NumberOfOpponents];
var WideningAgent:float = 2.0f;
var WallHeight : float = 10f;
var difficulty : float = 500f; // now the closer to ~400, not exponential. 200 makes an attractive legit track pretty often
 	
function Start () 
{	var bizzare :int = maxpoints;
	// init points
	for (var n:int=0;n<bizzare;n++)
	{
		var r1:float = Random.Range(0, 800.0) - 400;
		var r2:float = Random.Range(0, 800.0) - 400;		
		
		points[n]=Vector3(r1,0, r2);
		
	}
	PointsInConvexHull = findHull();
	var i : int = 0;
 	
 	//This area jimmy's with the shape, making it more complicated
 	var rSet : Vector3[] = new Vector3[PointsInConvexHull.length * 2];  
 	for(i = 0; i < PointsInConvexHull.length; ++i)  
 	{  
     	var randomPoint : Vector3 = new Vector3(Random.Range(0.0f, 0.1f), 0f, Random.Range(0.0f, 0.1f));
  
     	rSet[i*2] = PointsInConvexHull[i];  
     	rSet[i*2 + 1] = (((PointsInConvexHull[(i+1)%PointsInConvexHull.length] + PointsInConvexHull[i] )/2) + (randomPoint*difficulty));  
     	//Explaining: a mid point can be found with (dataSet[i]+dataSet[i+1])/2.  
    	 //Then we just add the displacement.  
 	}
 	//DrawConvexHull(PointsInConvexHull, Color.yellow);   
 	DrawConvexHull(PointsInConvexHull, Color.blue);      
 	PointsInConvexHull = rSet;  
 	//push apart again, so we can stabilize the points distances.  
 	//DrawConvexHull(PointsInConvexHull, Color.green); 
 	for(i = 0; i < 3; ++i)  
 	{  //
    	PointsInConvexHull= pullApart(PointsInConvexHull);  
	 	PointsInConvexHull = fixAngles(PointsInConvexHull);
	 }
	 //PointsInConvexHull = pullApart(PointsInConvexHull); 
	PointsInConvexHull = homogenizeProximatePoints(PointsInConvexHull, homogenizePointsDistanceThreshhold);   
	
	//DrawConvexHull(PointsInConvexHull, Color.blue);  
	
	newPointsInConvexHull = new Vector3[PointsInConvexHull.Length];
 	var countOfUnIdenticalPoints : int=0;
 	newPointsInConvexHull[0] = PointsInConvexHull[0];
 	
 	
 	for(var y : int = 1;y<PointsInConvexHull.Length-1;y++){
 		if(PointsInConvexHull[y]!=newPointsInConvexHull[countOfUnIdenticalPoints]){
 			if(PointsInConvexHull[y].z==0&&PointsInConvexHull[y].x==0){}else{
 				countOfUnIdenticalPoints++;
 				newPointsInConvexHull[countOfUnIdenticalPoints] = PointsInConvexHull[y];
 			}
 		}
 	}
 	
 	CleanedUpPoints = new Vector3[countOfUnIdenticalPoints];
 	
 	for(y = 0;y<countOfUnIdenticalPoints;y++){
 		CleanedUpPoints[y] = newPointsInConvexHull[y];
 	}
 	
 	
 	var Intersect : boolean;
 	//WHEW OK NOW WE GONA TEST FOR INTERSECTIONS> WHERE THERE ARE INTERSECTIONS
 	//cut the two line segments and any intervening segments, then bridge the two
 	//ie cut out all offensing Vertices and the remaining two vertices will be the new
 	// segment. This is an N^2-(N-1) type of search
 	for(var h : int = 0;h<CleanedUpPoints.Length-1;h++){
 	 	for(y = 0; y<CleanedUpPoints.Length-1;y++){
 	 		Intersect = FasterLineSegmentIntersection(
 	 			CleanedUpPoints[h],
 	 			CleanedUpPoints[(h+1)%CleanedUpPoints.Length],
 	 			CleanedUpPoints[y],
 	 			CleanedUpPoints[(y+1)%CleanedUpPoints.Length]
 	 			,h,y);
 	 		if(Intersect){
 	 		 for(var p : int = ((h+1)%CleanedUpPoints.Length);p<y;p++){
 	 		 	CleanedUpPoints[p] = (CleanedUpPoints[h] + CleanedUpPoints[(y+1)%CleanedUpPoints.Length])/2;//midway
 	 		 	 
 	 		 	}
 	 		 	h = y + 1;
 	 		 	y = y + CleanedUpPoints.Length;
 	 		 	
 	 		  }
 	 		}
 	 	
 	 }
 	//ONE last time, kill all duplicate Vertices
 	
 	newPointsInConvexHull = new Vector3[CleanedUpPoints.Length];
 	countOfUnIdenticalPoints =0;
 	newPointsInConvexHull[0] = CleanedUpPoints[0];
 	
 	
 	for(y =1;y<CleanedUpPoints.Length;y++){
 		if(CleanedUpPoints[y]!=newPointsInConvexHull[countOfUnIdenticalPoints]){
 			if(CleanedUpPoints[y].z==0&&CleanedUpPoints[y].x==0){}else{
 				countOfUnIdenticalPoints++;
 				newPointsInConvexHull[countOfUnIdenticalPoints] = CleanedUpPoints[y];
 			}
 		}
 	}
 	
 	 SecondCleanUpPoints = new Vector3[countOfUnIdenticalPoints+1];
 	
 	for(y = 0;y<countOfUnIdenticalPoints+1;y++){
 		SecondCleanUpPoints[y] = newPointsInConvexHull[y];
 	}
 	SecondCleanUpPoints[0] = (SecondCleanUpPoints[0] - (SecondCleanUpPoints[SecondCleanUpPoints.Length-1]*0.25));
  	//DrawConvexHull(SecondCleanUpPoints, Color.yellow);  
 	//Ok so no loops in the initial framework at least 
 	
 	curvedFrame = CatmullRom(SecondCleanUpPoints, samples, true);  //create catmullspline
 	
 	DrawConvexHull(curvedFrame, Color.cyan);  
 	
 	curvedFrame = TurnIntersectionIntoSmoothCurve(curvedFrame); 
 	//fix the end of the race clusterfuck
 	curvedFrame = SmoothEndOfRace(curvedFrame);
 	curvedFrame = cleanOutEqualVertices(curvedFrame);//Better clear out equal vertices
    curvedFrame = SmoothBeginningOfRace(curvedFrame);
    curvedFrame = TurnIntersectionIntoHomogenousPoints(curvedFrame);
    curvedFrame = cleanOutEqualVertices(curvedFrame);//Better clear out equal vertices
    
    
    DrawConvexHull(curvedFrame, Color.red);  
    
    ExteriorVertices = new Vector3[curvedFrame.Length];
    InteriorVertices = new Vector3[curvedFrame.Length];
   
    ExteriorVertices = ProjectRoadWayOutside(curvedFrame);
    InteriorVertices = ProjectRoadWayInside(curvedFrame);
    WayPoints = new Vector3[curvedFrame.Length];
    WayPoints = curvedFrame;
    
    BuildWayPointMeshes(WayPoints);
    
    //search Exterior and Interior verts for intersections, then cut offendinglooped off areas out, like we did before curvingng the track
    
 	ExteriorVertices = TurnIntersectionIntoSmoothCurve(ExteriorVertices); 	 
   	InteriorVertices = TurnIntersectionIntoSmoothCurve(InteriorVertices);
   	
 	TrackScaler = (TrackScaler * WideningAgent); //Change the widening parameters for the walls 
 	
 	var InsideWall : Vector3[] = new Vector3[InteriorVertices.Length]; //creating walls for the race
    var OutsideWall : Vector3[] = new Vector3[ExteriorVertices.Length]; 
    
 	OutsideWall = ProjectRoadWayOutside(ExteriorVertices);
    InsideWall = ProjectRoadWayInside(InteriorVertices);
    InsideWall = TurnIntersectionIntoSmoothCurve(InsideWall);
   	OutsideWall = TurnIntersectionIntoSmoothCurve(OutsideWall); 	 
   	
   	
    for(h = 0;h<InsideWall.Length;h++){
    	InsideWall[h].y = WallHeight;
	    
	    }
	for(h = 0;h<OutsideWall.Length;h++){
		OutsideWall[h].y = WallHeight;
		}
    DrawConvexHull(ExteriorVertices, Color.cyan);  
    DrawConvexHull(InteriorVertices, Color.green);  
    
    //NOW, we build a mesh from these vertices
 	CreateRaceTrack(InteriorVertices,ExteriorVertices, "Road", RoadMaterial);   
    CreateRaceTrack(ExteriorVertices, OutsideWall, "Outside Wall",  WallMaterial);
    CreateRaceTrack(InsideWall, InteriorVertices, "Inside Wall", WallMaterial);
    
    
    //SET RAACERS IN THE POLE POSITIONS
    EstablishStartingPositions(WayPoints, PolePositions);
    
}

function Update () {

}

function DrawConvexHull(PointsInConvexHull : Vector3[], variable : Vector4){
	if (variable == null){variable = Color.green;}
	for(var i : int = 1 ; i < PointsInConvexHull.Length ; i++) 
		{
			if (i>0){}	
			Debug.DrawLine (PointsInConvexHull[i-1], PointsInConvexHull[i], variable ,15, false);//  fOR DRAWINGS	
			Debug.DrawLine (PointsInConvexHull[i]-Vector3(0,0.5,0), PointsInConvexHull[i]+Vector3(0,0.5,0), Color.red,15,false);  // This creates a red grid we can see
			Debug.DrawLine (PointsInConvexHull[i]-Vector3(0.5,0,0), PointsInConvexHull[i]+Vector3(0.5,0,0), Color.red,15,false);
			Debug.DrawLine (PointsInConvexHull[i]-Vector3(0,0,0.5), PointsInConvexHull[i]+Vector3(0,0,0.5), Color.red,15,false); 
		
		}
		// last line
		Debug.DrawLine (PointsInConvexHull[0], PointsInConvexHull[PointsInConvexHull.Length-1], Color.yellow,15, false);
 }

function cleanOutEqualVertices(TheVertices:Vector3[]){
	var DummyVector :Vector3[] = new Vector3[TheVertices.Length];
	var countOfUnIdenticalPoints :int = 0;
	DummyVector[0] = TheVertices[0];
 	for(var y:int =1;y<TheVertices.Length;y++){
 		if(TheVertices[y]!=DummyVector[countOfUnIdenticalPoints]){
 			if(TheVertices[y].z==0&&TheVertices[y].x==0){}else{
 				countOfUnIdenticalPoints++;
 				DummyVector[countOfUnIdenticalPoints] = TheVertices[y];
 				}
 			}
 		}
 	var NewVertices : Vector3[] = new Vector3[countOfUnIdenticalPoints];
 	
 	for(y = 0;y<countOfUnIdenticalPoints;y++){
 		NewVertices[y] = DummyVector[y];
 	}
	return NewVertices;

}

function EstablishStartingPositions(WayPoints : Vector3[], PolePositions:Transform[]){
	var TheWayPoints : Vector3[] = WayPoints;
	var Positions : Vector3[] = new Vector3[NumberOfOpponents];
	var StartingSpot : Vector3 = (TheWayPoints[TheWayPoints.Length-1] + TheWayPoints[0])/2;
	var RightAngleVector : Vector3 = Vector3((-1*StartingSpot.z),StartingSpot.y,StartingSpot.x);
	var Increment = (StartingSpot -TheWayPoints[TheWayPoints.Length-1])/10;
	for(var y:int = 0;y<8;y++){
		Positions[y] =StartingSpot+(RightAngleVector*0.2);
		y++;
		Positions[y] =StartingSpot -(RightAngleVector*0.2);
		StartingSpot +=Increment;
		}
	for(y=0;y<8;y++){
		PolePositions[y].transform.position = Vector3(Positions[y].x,Positions[y].y+10,Positions[y].z);
		PolePositions[y].transform.LookAt(TheWayPoints[0]);
		}
	}




function TurnIntersectionIntoHomogenousPoints(TheVertices:Vector3[]){
	//WHEW OK NOW WE GONA TEST FOR INTERSECTIONS> WHERE THERE ARE INTERSECTIONS
 	//cut the two line segments and any intervening segments, then bridge the two
 	//ie cut out all offensing Vertices and the remaiSDFSADSDDSFning two vertices will be the new
 	// segment. This is an N^2-(N-1) type of search

	var Intersect:boolean;
	for(var h:int = 0;h<TheVertices.Length-1;h++){
 	 	//Debug.Log('Inside h:' + h );
 	 	for(var y:int = h+2; y<TheVertices.Length-1;y++){
 	 		Intersect = FasterLineSegmentIntersection(
 	 			TheVertices[h],
 	 			TheVertices[(h+1)%TheVertices.Length],
 	 			TheVertices[y],
 	 			TheVertices[(y+1)%TheVertices.Length]
 	 			,h,y);
 	 		if(Intersect){
 	 			Debug.Log('InterioVERTX' + h + ', y:' + y);
 	 		 for(var p : int = ((h+1)%TheVertices.Length);p<y;p++){
 	 		 	TheVertices[p] = (TheVertices[h] + TheVertices[(y+1)%TheVertices.Length])/2;//midway
 	 		 	 
 			 	}
 	 		 	h = y + 1;
 	 		 	y = y + TheVertices.Length;
 	 		 	
 	 		  }
 	 	} 	
 	}
 	 			
 	return TheVertices;
 }

function BuildWayPointMeshes(WP: Vector3[]){
	var sphere : GameObject[] = new GameObject[WP.Length];
	var name : String  = "";
	for(var h:int=0;h<WP.Length;h++){
		sphere[h] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere[h].transform.position = Vector3(WP[h].x,WP[h].y+3,WP[h].z);
		name = "WayPointSphere" + h.ToString();
		sphere[h].transform.name = name;
	}

} 
  
    
  
function TurnIntersectionIntoSmoothCurve(TheVertices:Vector3[]){
	//FIND the intersections in curverFrame; average vertices between them into a smooth curve
 	//reproces them. Find intersecting linesegments. The prio segment, start n-1 initial vertice
 	//for later segment, L+1 later vertice
 	// count vertices between them and then run them through
 	// curve making algorithm again! 
 	
	var Intersect:boolean;
 	var sizer :int;
 	var SmoothTheseVertices : Vector3[];
 	var gg :int;
 	var hgg :int;
 	
 	for(var h:int = 0;h<TheVertices.Length-1;h++){
 	 	for(var y:int = h+2; y<TheVertices.Length-1;y++){
 	 		Intersect = FasterLineSegmentIntersection(
 	 			TheVertices[h],
 	 			TheVertices[(h+1)%TheVertices.Length],
 	 			TheVertices[y],
 	 			TheVertices[(y+1)%TheVertices.Length]
 	 			,h,y);
 	 		if(Intersect){
 	 
 	 		 	sizer = Mathf.Abs(y+1-h);
 				SmoothTheseVertices = new Vector3[sizer];
 				for(gg=h;gg<y+1;gg++){
 					//hgg = h-gg;
 					////Debug.Log("h-gg: " + hgg + ", gg:" + gg);
 					SmoothTheseVertices[gg-h] = TheVertices[gg]; 
 				} 
 				SmoothTheseVertices = CatmullRom(SmoothTheseVertices, samples, true);
 				for(gg=h;gg<y+1;gg++){
 					TheVertices[gg] = SmoothTheseVertices[gg-h]; 
 				} 		 	
 	 		 	h = y + 1;
 	 		 	y = y + TheVertices.Length;
 	 		 	
 	 		 }
 	 	}	
 	 }
 	 return TheVertices;
}    
function SmoothEndOfRace(TheVertices:Vector3[]){
	/*
	var EndOfRaceVertices : Vector3[] = new Vector3[4];
	var NewVertices : Vector3[];
	var newEndOfRaceVertices : Vector3[];
	//take straight away that starts race, cut it by 1/4 to us for curving
	
	var TruncatedStraightway : Vector3 = ((TheVertices[TheVertices.Length-1]*3)+ TheVertices[TheVertices.Length-2])/4;
	TheVertices[TheVertices.Length-2] = TruncatedStraightway;
	var LastLineSegmentEnd : Vector3 = ((TheVertices[TheVertices.Length-2]*3)+ TheVertices[TheVertices.Length-3])/4;
	EndOfRaceVertices[0] = LastLineSegmentEnd;
	EndOfRaceVertices[1] = LastLineSegmentEnd;
	EndOfRaceVertices[2] = TruncatedStraightway;
	EndOfRaceVertices[3] = TruncatedStraightway;
	TheVertices[TheVertices.Length-2] = TruncatedStraightway;
	TheVertices[TheVertices.Length-3] = LastLineSegmentEnd;
	 
	//newEndOfRaceVertices = CatmullRom(EndOfRaceVertices,5,true);
	//Debug.Log("return from catmulrom length: "+newEndOfRaceVertices.Length);
	//NewVertices = new Vector3[newEndOfRaceVertices.Length + TheVertices.Length - 2];
	/*
	for(var y:int = 0;y<TheVertices.Length-3;y++){
		NewVertices[y]=TheVertices[y];
	}
	for(var r:int = y+1; r<newEndOfRaceVertices.Length + TheVertices.Length - 2;r++){
		Debug.Log("indices of r: " + r + "; y:" + y + '; NewVertices.length='+NewVertices.Length + "; newEndOfRaceVertices.Length="+newEndOfRaceVertices.Length+"; TheVertices.Length="+TheVertices.Length);
		NewVertices[r] = newEndOfRaceVertices[r-(y+1)];

	}*/
	/*for(var g:int=0;g<NewVertices.Length;g++){
		if(NewVertices[g].x==0&&NewVertices[g].z==0){
			NewVertices[g]=((NewVertices[((g+NewVertices.Length)-1)%NewVertices.Length])+(NewVertices[(g+1)%NewVertices.Length]))/2;
		}
	}*/
	return TheVertices;

}
function SmoothBeginningOfRace(TheVertices:Vector3[]){
	/*
	var BeginningOfRaceVertices : Vector3[] = new Vector3[4];
	var NewVertices : Vector3[];
	var newEndOfRaceVertices : Vector3[];
	//take straight away that starts race, cut it by 1/4 to us for curving
	
	var TruncatedStraightway : Vector3 = ((TheVertices[TheVertices.Length-1]*3)+ TheVertices[TheVertices.Length-2])/4;
	TheVertices[TheVertices.Length-2] = TruncatedStraightway;
	var LastLineSegmentEnd : Vector3 = ((TheVertices[TheVertices.Length-2]*3)+ TheVertices[TheVertices.Length-3])/4;
	EndOfRaceVertices[0] = LastLineSegmentEnd;
	EndOfRaceVertices[1] = LastLineSegmentEnd;
	EndOfRaceVertices[2] = TruncatedStraightway;
	EndOfRaceVertices[3] = TruncatedStraightway;
	TheVertices[TheVertices.Length-2] = TruncatedStraightway;
	TheVertices[TheVertices.Length-3] = LastLineSegmentEnd;
	 
	//newEndOfRaceVertices = CatmullRom(EndOfRaceVertices,5,true);
	//Debug.Log("return from catmulrom length: "+newEndOfRaceVertices.Length);
	//NewVertices = new Vector3[newEndOfRaceVertices.Length + TheVertices.Length - 2];
	/*
	for(var y:int = 0;y<TheVertices.Length-3;y++){
		NewVertices[y]=TheVertices[y];
	}
	for(var r:int = y+1; r<newEndOfRaceVertices.Length + TheVertices.Length - 2;r++){
		Debug.Log("indices of r: " + r + "; y:" + y + '; NewVertices.length='+NewVertices.Length + "; newEndOfRaceVertices.Length="+newEndOfRaceVertices.Length+"; TheVertices.Length="+TheVertices.Length);
		NewVertices[r] = newEndOfRaceVertices[r-(y+1)];

	}*/
	/*for(var g:int=0;g<NewVertices.Length;g++){
		if(NewVertices[g].x==0&&NewVertices[g].z==0){
			NewVertices[g]=((NewVertices[((g+NewVertices.Length)-1)%NewVertices.Length])+(NewVertices[(g+1)%NewVertices.Length]))/2;
		}
	}*/
	return TheVertices;

}


function findHull()
{
	var n:int = points.length;

	var rc : RaycastComp2 = new RaycastComp2();
	System.Array.Sort(points, rc);
	
	var ans:Vector3[] = new Vector3[2 * n];		        // In between we may have a 2n points
	var k:int = 0;
	var start:int = 0;					// start is the first insertion point
 
	for(var i:int = 0; i < n; i ++)                     // Finding lower layer of hull
	{
		var p:Vector3 = points[i];	
		//print (i+" : "+p);
		while ( k - start >= 2 && xcross(ans[k-2],ans[k-1],p) <= 0)
			k--;			
		ans[k++] = p; 
	}
	k--; 						// drop off last point from lower layer
	start = k ;						
 
	for(i = n-1 ; i >= 0 ; i --)                // Finding top layer from hull
	{
		p = points[i];
		while ( k - start >= 2 && xcross(ans[k-2],ans[k-1],p) <= 0)
			k--;
		ans[k++] = p; 
	}
	k--;						// drop off last point from top layer
	var returnThis : Vector3[] = new Vector3[k]; 
 	for(var r : int = 0; r<k;r++){
 		returnThis[r] = ans[r];
 	}
 
	return returnThis;                   // convex hull is of size k	
}

function  xcross(O:Vector3,A:Vector3,B:Vector3):float
{
	return (A.x - O.x) * (B.z - O.z) - (A.z - O.z) * (B.x - O.x);
}

function  ccross(p1:Vector3,p2:Vector3):float
{
		return p1.x * p2.z - p1.z * p2.x;
}

function ssub(p1:Vector3,p2:Vector3):Vector3
{
	return Vector3(p1.x - p2.x, p1.z - p2.z);
}

function bubbleSort()
{
    var continueSort = true;
    if(points.length >1)
	{
        while (continueSort)
		{
            for(var i=0;i<points.length;i++)
            {
                for(var j=i+1;j<points.length;j++)
                {
                    if(points[i].x > points[j].x)
                    {
                        continueSort = true;
                        var leftMost=points[i];
                        points[i]=points[j];
                        points[j]=leftMost;
                    }
                    else continueSort = false;
                }
            }
        }
    }
}


//function compareTo(a:Vector3,other:Vector3):float
function xcompareTo(a:Vector3,other:Vector3):float
{
//	if(a.x == other.x) 
	if(a.x > other.x) 
	{
		//print ("byZ:"+a.x+"<>"+other.x+ " = " + (a.z - other.z));
//		return Mathf.Abs(a.z - other.z);
//		return Mathf.Min(a.z,other.z);
		return 0;
	}else {
//		print ("byX:"+a.x+"<>"+other.x+ " = " + Mathf.Abs(a.x - other.x));
//		return Mathf.Abs(a.x - other.x);
//		if (a.x>other.x)
		//return Mathf.Min(a.x,other.x);
		return 1;
	}
}


class RaycastComp2 implements IComparer
{
    function Compare(x : System.Object, y : System.Object) : int
    {
        //if ( !(x instanceof RaycastHit) || !(y instanceof RaycastHit)) return;
        var raycastHitX : Vector3 = x;
        var raycastHitY : Vector3 = y;
		
		if (raycastHitX.x==raycastHitY.x)
			return raycastHitX.z.CompareTo(raycastHitY.z);
		else
			return raycastHitX.x.CompareTo(raycastHitY.x);
    }
}

function fixAngles(dataSet:Vector3[] )  
 {  
     for(var i = 0; i < dataSet.length; i++){  
         if(i-1<0)
         	var previous : int = dataSet.length-1;
         else 
         	previous  = i-1;
           
         var next : int = ((i+1) % dataSet.length);  
         var px : float = dataSet[i].x - dataSet[previous].x;  
         var py : float = dataSet[i].z - dataSet[previous].z;  
         var pl : float = Mathf.Sqrt(px*px + py*py);  
         px = px/pl;  
         py = py/pl;  
           
         var nx : float = dataSet[i].x - dataSet[next].x; 
         var ny :float = dataSet[i].z - dataSet[next].z;  
         nx = -nx;  
         ny = -ny;  
         var nl : float = Mathf.Sqrt(nx*nx + ny*ny);  
         nx = nx/nl;  
         ny = nx/nl;  
         //I got a vector going to the next and to the previous points, normalised.  
   
         var a : float= Mathf.Atan2(px * ny - py * nx, px * nx + py * ny); 
         // perp dot product between the previous and next point. Google it you should learn about it!  
   
         if(Mathf.Abs(a * Mathf.Rad2Deg) <= 100) continue;  
   
         var nA : float = 100 * Mathf.Sign(a) * Mathf.Deg2Rad;  
         var diff : float = nA - a;  
         var cos : float = Mathf.Cos(diff);  
         var sin : float = Mathf.Sin(diff);  
         var newX : float = nx * cos - ny * sin;  
         var newY : float = nx * sin + ny * cos;  
         newX = newX * nl;  
         newY = newY * nl;  
         dataSet[next].x = dataSet[i].x + newX;  
         dataSet[next].z = dataSet[i].z + newY;  
         //I got the difference between the current angle and 100degrees, and built a new vector that puts the next point at 100 degrees.  
         return dataSet;
     }  
 }

function pullApart(dataSet:Vector3[] )  
 {  
     var dst : float = 5; //I found that 15 is a good value, though maybe, depending on your scale you'll need other value.  
     var dst2 : float = dst*dst;  
     for(var i : int = 0; i < dataSet.length; ++i)  
     {  
         for(var j : int = i+1; j < dataSet.length; ++j)  
         {  
             if((dataSet[i]-(dataSet[j])).magnitude < dst2)  
             {  
                 var hx :float = dataSet[j].x - dataSet[i].x;  
                 var hy :float = dataSet[j].z - dataSet[i].z;  
                 var hl : float = Mathf.Sqrt(hx*hx + hy*hy);  
                 hx = hx/hl;  
                 hy = hy/hl;  
                 var dif : float = dst - hl;  
                 hx *= dif;  
                 hy *= dif;  
                 dataSet[j].x += hx;  
                 dataSet[j].z += hy;  
                 dataSet[i].x -= hx;  
                 dataSet[i].z -= hy;  
             }  
         }  
     } 
     return dataSet; 
 } 
 
 function homogenizeProximatePoints(collectionOfPoints : Vector3[], AcceptableDistance : int){
 	//homogenizePointsDistanceThreshhold
 	var newPoint : Vector3;
 	var holdingPoint : Vector3;
 	var secondHoldingPoint : Vector3;
 	
 	for(var f : int = 0;f<collectionOfPoints.Length-1;f++){
 		holdingPoint = collectionOfPoints[f] - collectionOfPoints[f+1];
 		if(holdingPoint.magnitude<AcceptableDistance){
 			newPoint = (collectionOfPoints[f]+collectionOfPoints[f+1])/2;
 			collectionOfPoints[f] = newPoint;
 			collectionOfPoints[(f+1)%collectionOfPoints.Length] = newPoint;
 		}
 		
 	}
 	
 	for( f = 0;f<collectionOfPoints.Length-1;f++){
 		holdingPoint = collectionOfPoints[f] - collectionOfPoints[(f+2)%collectionOfPoints.Length];
 		secondHoldingPoint = collectionOfPoints[f] - collectionOfPoints[f+1];
 		if(holdingPoint.magnitude<AcceptableDistance&&secondHoldingPoint.magnitude>AcceptableDistance){
 			newPoint = (collectionOfPoints[f]+collectionOfPoints[f+1])/2;
 			collectionOfPoints[f] = newPoint;
 			collectionOfPoints[(f+1)%collectionOfPoints.Length] = newPoint;
 		}
 		
 	}
 	
 	
 	return collectionOfPoints;	
 }
 
 
    
/// <summary>
/// Takes an array of input coordinates used to define a Catmull-Rom spline, and then
/// samples the resulting spline according to the specified sample count (per span),
/// populating the output array with the newly sampled coordinates. The returned boolean
/// indicates whether the operation was successful (true) or not (false).
/// NOTE: The first and last points specified are used to describe curvature and will be dropped
/// from the resulting spline. Duplicate them if you wish to include them in the curve.
///   THE DUPLICATION IS NOW IN THE FUNCTION ITSELF
/// </summary>
function CatmullRom(inCoordinates : Vector3[],   samples : int, AppendValues : boolean)
{	
	var processCoordinates : Vector3[] = new Vector3[inCoordinates.Length + 2];
	processCoordinates[0] = inCoordinates[inCoordinates.Length-1];    //duplicating
	for(var bs : int = 0;bs<inCoordinates.Length - 1;bs++){
		processCoordinates[bs+1] = inCoordinates[bs];
	}
	processCoordinates[bs+2] = inCoordinates[0];
	if(!AppendValues){
		processCoordinates = new Vector3[inCoordinates.Length + 2];
		processCoordinates = inCoordinates;
	}
	if (inCoordinates.Length < 4)
	{
		return inCoordinates;
	}
 	var lengthOfArray : int = (samples * inCoordinates.Length); //-samples/2;
	var results : Vector3[] = new Vector3[lengthOfArray];
 	var indice : int = 0;
 	
	for (var n : int = 1; n < inCoordinates.Length; n++)
		for (var i : int  = 0; i < samples; i++){
			results[indice++] = PointOnCurve(processCoordinates[(n - 1)%processCoordinates.Length], processCoordinates[n%processCoordinates.Length], processCoordinates[(n + 1)%processCoordinates.Length], processCoordinates[(n + 2)%processCoordinates.Length], (1f / samples) * i,  inCoordinates[inCoordinates.Length - 1]);
 			
 			}
 			
	results[lengthOfArray-1] = inCoordinates[inCoordinates.Length - 1];
	var resultsTruncated : Vector3[] = new Vector3[results.Length-samples];
	for(var k : int = 0; k<resultsTruncated.Length-1;k++){
		resultsTruncated[k] = results[k];
		}
	for(var g : int=1; g<((samples));g++){	
		resultsTruncated[resultsTruncated.Length-g] = results[results.Length-1]; 
		 
		}
	//resultsTruncated[resultsTruncated.Length-3] = results[results.Length-3]; 
	
	return resultsTruncated;
	
}
 
/// <summary>
/// Return a point on the curve between P1 and P2 with P0 and P4 describing curvature, at
/// the normalized distance t.
/// </summary>
 
function PointOnCurve(p0 : Vector3, p1 : Vector3, p2 : Vector3, p3 : Vector3, t : float, protector : Vector3)
{
	var result : Vector3;
	
 
	var t0 : float = ((-t + 2f) * t - 1f) * t * 0.5f;
	var t1 : float = (((3f * t - 5f) * t) * t + 2f) * 0.5f;
	var t2 : float = ((-3f * t + 4f) * t + 1f) * t * 0.5f;
	var t3 : float = ((t - 1f) * t * t) * 0.5f;
 
 	
	result.x = p0.x * t0 + p1.x * t1 + p2.x * t2 + p3.x * t3;
	result.y = p0.y * t0 + p1.y * t1 + p2.y * t2 + p3.y * t3;
	result.z = p0.z * t0 + p1.z * t1 + p2.z * t2 + p3.z * t3;
 	if(result.x==0&&result.z==0){
 		result.x = protector.x;
 		result.z = protector.z;
 		////Debug.Log("<color=red>Fatal error:</color> n:" + n + "; i:" + i + "; indice: " + indice);
 	}
	return result;
}   		

function FasterLineSegmentIntersection(p1 : Vector3,  p2:Vector3, p3 : Vector3, p4 : Vector3, h :int, y:int ) {
       
        var a : Vector3 = p2 - p1;
        var b : Vector3 = p3 - p4;
        var c : Vector3 = p1 - p3;
       
        var alphaNumerator : float = b.z*c.x - b.x*c.z;
        var alphaDenominator : float = a.z*b.x - a.x*b.z;
        var betaNumerator : float = a.x*c.z - a.z*c.x;
        var betaDenominator : float = a.z*b.x - a.x*b.z;
       
        var doIntersect : boolean = true;
       
        if (alphaDenominator == 0 || betaDenominator == 0) {
            doIntersect = false;
        } else {
           
            if (alphaDenominator > 0) {
                if (alphaNumerator < 0 || alphaNumerator > alphaDenominator) {
                    doIntersect = false;
                   
                }
            } else if (alphaNumerator > 0 || alphaNumerator < alphaDenominator) {
                doIntersect = false;
            }
           
            if (doIntersect && betaDenominator > 0) {
                if (betaNumerator < 0 || betaNumerator > betaDenominator) {
                    doIntersect = false;
                }
            } else if (betaNumerator > 0 || betaNumerator < betaDenominator) {
                doIntersect = false;
            }
        }
     
        return doIntersect;
    }
    
    function ProjectRoadWayInside(SplinesSpine : Vector3[]){
		var InsideVertice : Vector3[] = new Vector3[SplinesSpine.Length];    
		//var OutsideVertice : Vector3 = new Vector3[SplinesSpine.Length];    
		var deltaZ : float;	
		var deltaX : float;
		var derevative : float;
		var DZ : Vector3;
		var SlopeAtPoint:Vector3;
		
		for(var f : int = 0;f<SplinesSpine.Length;f++){
   			deltaZ = SplinesSpine[((f-1)+SplinesSpine.Length)%SplinesSpine.Length].z - SplinesSpine[(f+1)%SplinesSpine.Length].z;
   			deltaX = SplinesSpine[((f-1)+SplinesSpine.Length)%SplinesSpine.Length].x - SplinesSpine[(f+1)%SplinesSpine.Length].x;
   			
   			SlopeAtPoint = new Vector3(-deltaZ,0,deltaX);
   			
   			InsideVertice[f] = SplinesSpine[f] - (SlopeAtPoint.normalized * TrackScaler);
   		
   			
     	}
     	InsideVertice = TurnIntersectionIntoSmoothCurve(InsideVertice);
     	InsideVertice[SplinesSpine.Length-2] = InsideVertice[SplinesSpine.Length-1];
     	InsideVertice[SplinesSpine.Length-3] = InsideVertice[SplinesSpine.Length-1];
     	InsideVertice = TurnIntersectionIntoHomogenousPoints(InsideVertice);
     	
     	return InsideVertice;
     	
    }
    function ProjectRoadWayOutside(SplinesSpine : Vector3[]){
	//	var InsideVertice : Vector3 = new Vector3[SplinesSpine.Length];    
		var OutsideVertice : Vector3[] = new Vector3[SplinesSpine.Length];    
		var deltaZ : float;	
		var deltaX : float;
		var derevative : float;
		var SlopeAtPoint : Vector3;
		var deb : int;
		
		for(var f : int = 0;f<SplinesSpine.Length;f++){
			
   			deltaZ = SplinesSpine[((f-1)+SplinesSpine.Length)%SplinesSpine.Length].z - SplinesSpine[(f+1)%SplinesSpine.Length].z;
   			deltaX = SplinesSpine[((f-1)+SplinesSpine.Length)%SplinesSpine.Length].x - SplinesSpine[(f+1)%SplinesSpine.Length].x;
   			
   			SlopeAtPoint = new Vector3(-deltaZ,0,deltaX);
   			
   			OutsideVertice[f] = SplinesSpine[f] + (SlopeAtPoint.normalized * TrackScaler);	
   			
     	}
     	OutsideVertice = TurnIntersectionIntoSmoothCurve(OutsideVertice);
     	OutsideVertice[SplinesSpine.Length-2] = OutsideVertice[SplinesSpine.Length-1];
     	return OutsideVertice;
     	
    }
    
function CreateRaceTrack(InsideV:Vector3[],OutsideV:Vector3[], Name:String, C: Material) {

var TrackVertices : Vector3[] = new Vector3[InsideV.Length + OutsideV.Length];
var TrackVCount : int = 0;
//InsideV[InsideV.Length-2] = InsideV[InsideV.Length-1];
//OutsideV[OutsideV.Length-2] = OutsideV[OutsideV.Length-1];
for(var t:int = 0;t<InsideV.Length;t++){
	TrackVertices[TrackVCount] = InsideV[t];
	TrackVCount++;
	TrackVertices[TrackVCount] = OutsideV[t];
	TrackVCount++;
}
  
var ThisTrack : GameObject = new GameObject (Name); //create an empty gameobject with that name
 
ThisTrack.AddComponent(MeshFilter); //add a meshfilter
ThisTrack.AddComponent(MeshRenderer); 
ThisTrack.AddComponent(MeshCollider); 
//ThisTrack.AddComponent(Rigidbody);
ThisTrack.tag = Name;

var TriangleCount:int= 0; 
var newTriangles : int[] = new int[TrackVertices.Length*3];
for(t=0;t<TrackVertices.Length;t++){
	if(t%2==1){
	newTriangles[TriangleCount] = t;
	TriangleCount++;
	newTriangles[TriangleCount] = (t+1)%TrackVertices.Length;
	TriangleCount++;
	newTriangles[TriangleCount] = (t+2)%TrackVertices.Length;
	TriangleCount++;}
	else{
	newTriangles[TriangleCount] = (t+1)%TrackVertices.Length;
	TriangleCount++;
	newTriangles[TriangleCount] = t;
	TriangleCount++;
	newTriangles[TriangleCount] = (t+2)%TrackVertices.Length;
	TriangleCount++;}
		
}

var UVs : Vector2[] = new Vector2[TrackVertices.Length];
for(t=0;t<TrackVertices.Length;t++){
	UVs[t] =new Vector2(TrackVertices[t].x,TrackVertices[t].z); 
}

var newMesh : Mesh = new Mesh (); //create a new mesh, assign the vertices and triangles
newMesh.vertices = TrackVertices;
newMesh.triangles = newTriangles;
newMesh.uv = UVs;
newMesh.RecalculateNormals(); //recalculate normals, bounds and optimize
newMesh.RecalculateBounds();
newMesh.Optimize();
ThisTrack.isStatic = true;
(ThisTrack.GetComponent(MeshFilter) as MeshFilter).mesh = newMesh; 
(ThisTrack.GetComponent(MeshCollider) as MeshCollider).mesh = newMesh; 

(ThisTrack.GetComponent(MeshRenderer) as MeshRenderer).renderer.material = C; 

//(ThisTrack.GetComponent(Rigidbody) as Rigidbody).useGravity = false; 
//(ThisTrack.GetComponent(Rigidbody) as Rigidbody).freezeRotation = true; 
 
}