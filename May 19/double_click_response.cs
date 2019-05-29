
//set the atttribute of event when you click the componnet.
        public override void CreateAttributes()
        {
            m_attributes = new AttributesA(this);
        }

        private class AttributesA : GH_ComponentAttributes{
            public AttributesA(IGH_Component component) : base(component) { }

            public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
            {
                SomeWindow window = new SomeWindow((UiAttempt3Component)Owner);
                window.Show();


                return GH_ObjectResponse.Handled;
            }


            public override GH_ObjectResponse RespondToKeyDown(GH_Canvas sender, KeyEventArgs e)
            {
                    SomeWindow window = new SomeWindow((UiAttempt3Component)Owner);
                    window.Show();

                return GH_ObjectResponse.Handled;
            }

        }