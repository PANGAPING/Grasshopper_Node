  private void RunScript(ref object A)
  {

    var map = new Dictionary<int,List<Guid>>();

    foreach(var obj in RhinoDoc.ActiveDoc.Objects){
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

    A = tree;



    //var tree = new DataTree <Guid> ();
    //foreach( var g in groups ) {
    //var something = g.Select(id => id);
    //tree.AddRange(g.Select(id => id), new GH_Path (g.Key));
    //}
    //A = groups;
    //A = tree;
  }