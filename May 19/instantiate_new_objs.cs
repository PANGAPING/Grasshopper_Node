  private void RunScript(bool x, List<object> y, ref object A)
  {

    Random rnd = new Random();

    if(x)
    {
      //instantiate  new slider
      Grasshopper.Kernel.Special.GH_NumberSlider slid = new Grasshopper.Kernel.Special.GH_NumberSlider();
      slid.CreateAttributes(); //sets up default values, and makes sure your slider doesn't crash rhino
      
      //customise slider (position, ranges etc)
      int inputcount = this.Component.Params.Input[1].SourceCount;
      slid.Attributes.Pivot = new PointF((float) this.Component.Attributes.DocObject.Attributes.Bounds.Left - slid.Attributes.Bounds.Width - 30, (float) this.Component.Params.Input[1].Attributes.Bounds.Y + inputcount * 30);
      slid.Slider.Maximum = 10;
      slid.Slider.Minimum = 0;
      slid.Slider.DecimalPlaces = 2;
      slid.SetSliderValue((decimal) (rnd.Next(1000) * 0.01));
      
      //Until now, the slider is a hypothetical object.
      // This command makes it 'real' and adds it to the canvas.
      GrasshopperDocument.AddObject(slid, false);

      //Connect the new slider to this component
      this.Component.Params.Input[1].AddSource(slid);
    }

  }