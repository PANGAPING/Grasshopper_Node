  
//Reference to ladybug



  private void RunScript(DataTree<Brep> Geometries, double Grid_Size, List<Vector3d> Sun_Vectors, List<object> Context_Geometries, ref object MeshSufs, ref object TestPoints, ref object SunLightHours)
  {
    // List<Mesh> analysisMeshes;
    //List<Brep> analysisBreps;
    //Divide_Meshes_Breps(Geometries.AllData(), out analysisMeshes, out analysisBreps);


    //Convert brep to meshes according grid size.


    DataTree<Mesh> meshSufs = new DataTree<Mesh>();
    DataTree<Point3d> testPoints = new DataTree<Point3d>();
    DataTree<double> sunLightHours = new DataTree<double>();


    List<List<Mesh>> meshedSufs = Convert_Brep_To_Mesh(Geometries.AllData(), Grid_Size);


    //Get all the mesh into one, for quick intersection judge.
    Mesh shelter = GetShelterMesh(meshedSufs);


    for(int i = 0;i < meshedSufs.Count;i++){
      for(int j = 0; j < meshedSufs[i].Count;j++){
        List<Point3d> points = Get_TestPoints_From_Mesh(meshedSufs[i][j]);
        GH_Path path = new GH_Path(new int[2]{i,j});

        testPoints.AddRange(points, path);
        sunLightHours.AddRange(Analysis_Sun_Hours(points, shelter, Sun_Vectors), path);
      }
    }





    TestPoints = testPoints;

    SunLightHours = sunLightHours;


    //for(int i = 0;i < meshes.Count;i++){
    //output.AddRange(meshes[i], new GH_Path(i));
    //}


    //Print(Geometries.AllData().Count.ToString());

    //A = output;


    //Get the test_points
    //List<Point3d> test_Points = Get_Test_Points_From_Mesh(Mesh meshFace);

  }

  // <Custom additional code> 
  //private void Divide_Meshes_Breps(List<GeometryBase> geometries, out List<Mesh> outputMeshes, out List<Brep> outputBreps){
  //foreach (GeometryBase geometry in geometries){
  //if(geometry)
  //}
  //}



  private Mesh GetShelterMesh(List<List<Mesh>> meshes){
    Mesh output = new Mesh();

    for(int i = 0 ; i < meshes.Count;i++){
      for(int j = 0; j < meshes[i].Count;j++){
        output.Append(meshes[i][j]);
      }
    }

    return output;
  }



  private List<Point3d> Get_TestPoints_From_Mesh(Mesh meshSuf){
    List<Point3d> output = new List<Point3d>();

    //Offset coeffient the test points along the face normal vector.
    float testPointMoveEff = 0.5F;

    meshSuf.FaceNormals.ComputeFaceNormals();
    meshSuf.FaceNormals.UnitizeFaceNormals();

    for (int i = 0;i < meshSuf.Faces.Count ; i++){
      Vector3f moveVector = Vector3f.Multiply(meshSuf.FaceNormals[i], testPointMoveEff);
      Point3d testPoint = new Point3d(meshSuf.Faces.GetFaceCenter(i)) + moveVector;
      output.Add(testPoint);
    }

    return output;
  }



  private List<List<Mesh>> Convert_Brep_To_Mesh(List<Brep> geometries, double gridSize){
    List<List<Mesh>> output = new List<List<Mesh>>();

    //Set the max_min edge length for the mesh.
    MeshingParameters meshConfig = new MeshingParameters();
    meshConfig.MinimumEdgeLength = gridSize;
    meshConfig.MaximumEdgeLength = gridSize;
    meshConfig.GridAspectRatio = 1;


    foreach (Brep geometry in geometries){
      output.Add(Mesh.CreateFromBrep(geometry, meshConfig).ToList());
    }
    return output;
  }


  private List<double> Analysis_Sun_Hours(List<Point3d> testPoints, Mesh shelter, List<Vector3d> sunVectors){
    List<double> output = new List<double>();

    // for(int i = 0; i < sunVectors.Count; i++){
    // Ray3d = Ray3d()

    // }


    foreach(Vector3d vector in sunVectors){
      vector.Reverse();
    }

    for(int i = 0;i < testPoints.Count;i++){

      double hours = 0;

      for(int j = 0;j < sunVectors.Count;j++){
        int sunOk = 1;

        Ray3d ray = new Ray3d(testPoints[i], sunVectors[j]);
        if(Rhino.Geometry.Intersect.Intersection.MeshRay(shelter, ray) > 0){
          sunOk = 0;
        }
        hours += sunOk;
      }

      output.Add(hours);
    }


    return output;
  }


  // </Custom additional code> 
}