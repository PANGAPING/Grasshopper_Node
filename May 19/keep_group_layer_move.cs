  
//This just is a piece of code let you move follow the house number,layer and window group.
//Just for the project I am working on.

 private void RunScript(DataTree<GeometryBase> Geometries, List<Vector3d> VectorXYs, DataTree<Vector3d> VectorZt, ref object MovedGeometries)
  {
    DataTree<GeometryBase> output = new DataTree<GeometryBase>();



    //The i can identify the house number
    for(int i = 0; i < VectorXYs.Count;i++){


      //Store the current xy vector(which means the house number)
      Vector3d curXYVector = VectorXYs[i];

      //K identifies the layer
      for(int k = 0; k < VectorZt.Branch(VectorZt.Paths[i]).Count;k++){

        Vector3d curZVector = VectorZt.Branch(VectorZt.Paths[i])[k];

        //l identifies the windos group count
        for(int l = 0; l < Geometries.Paths.Count;l++){

          // (house number, layer number, window group)
          int[] path = new int[]{i,k,l};

          GH_Path windowPath = new GH_Path(path);

          List < GeometryBase > translatedGeometries =
            Geometries.Branch(Geometries.Paths[l]).Select(x => {return x.Duplicate();}).Select(x => {x.Translate(curXYVector + curZVector); return x;}).ToList();

          // Add data to the branch.

          output.AddRange(translatedGeometries, windowPath);

        }
      }
    }

    MovedGeometries = output;
  }