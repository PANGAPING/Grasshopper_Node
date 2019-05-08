/// <summary>
/// This component will return the grouped reference object which you have grouped in rhino.
/// Assume your groups have just one layer.
/// </summary>


  private void RunScript(List<Guid> geometries, ref object grouped_geometries)
  {
    //The inputs need to be reference objects  meaning it is picked by hand and stored in some parameters battery.
    //So it can be convert into Guid object.
    List<RhinoObject> objs = new List<RhinoObject>();

    var map = new Dictionary<int,List<Guid>>();


    foreach (Guid guid in geometries){
      // Only RhinoObject class has GetGroupList method, so we transfer ObjRef class to RhinoObject class. 
      //Using Object method.
      RhinoObject obj = new ObjRef(guid).Object();
      if (obj.GetGroupList() != null){
        if(map.ContainsKey(obj.GetGroupList()[0]) == false){
          map.Add(obj.GetGroupList()[0], new List<Guid>());
        }
        map[obj.GetGroupList()[0]].Add(obj.Id);
      }
    }




    var tree = new DataTree<Guid>();

    foreach (var groupIndex in map.Keys){
      tree.AddRange(map[groupIndex], new GH_Path(groupIndex));
    }

    grouped_geometries = tree;
  }
