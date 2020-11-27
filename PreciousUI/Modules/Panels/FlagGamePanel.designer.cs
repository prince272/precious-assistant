using DevExpress.XtraMap;
using System.Drawing;

namespace PreciousUI.Modules.Panels
{
    partial class FlagGamePanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            OnDispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraMap.KeyColorColorizer keyColorColorizer1 = new DevExpress.XtraMap.KeyColorColorizer();
            DevExpress.XtraMap.AttributeItemKeyProvider attributeItemKeyProvider1 = new DevExpress.XtraMap.AttributeItemKeyProvider();
            DevExpress.XtraMap.ColorizerKeyItem colorizerKeyItem1 = new DevExpress.XtraMap.ColorizerKeyItem();
            DevExpress.XtraMap.ColorizerKeyItem colorizerKeyItem2 = new DevExpress.XtraMap.ColorizerKeyItem();
            DevExpress.XtraMap.ColorizerKeyItem colorizerKeyItem3 = new DevExpress.XtraMap.ColorizerKeyItem();
            DevExpress.XtraMap.ColorizerKeyItem colorizerKeyItem4 = new DevExpress.XtraMap.ColorizerKeyItem();
            DevExpress.XtraMap.GeoMapCoordinateSystem geoMapCoordinateSystem1 = new DevExpress.XtraMap.GeoMapCoordinateSystem();
            DevExpress.XtraMap.MillerProjection millerProjection1 = new DevExpress.XtraMap.MillerProjection();
            this.FileLayer = new DevExpress.XtraMap.VectorItemsLayer();
            this.ShapefileDataAdapter = new DevExpress.XtraMap.ShapefileDataAdapter();
            this.mapControl1 = new DevExpress.XtraMap.MapControl();
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).BeginInit();
            this.SuspendLayout();
            attributeItemKeyProvider1.AttributeName = "Answer";
            keyColorColorizer1.ItemKeyProvider = attributeItemKeyProvider1;
            colorizerKeyItem1.Key = "Wrong";
            colorizerKeyItem1.Name = "Wrong";
            colorizerKeyItem2.Key = "None";
            colorizerKeyItem2.Name = "None";
            colorizerKeyItem3.Key = "Try";
            colorizerKeyItem3.Name = "Try";
            colorizerKeyItem4.Key = "Right";
            colorizerKeyItem4.Name = "Right";
            keyColorColorizer1.Keys.Add(colorizerKeyItem1);
            keyColorColorizer1.Keys.Add(colorizerKeyItem2);
            keyColorColorizer1.Keys.Add(colorizerKeyItem3);
            keyColorColorizer1.Keys.Add(colorizerKeyItem4);
            keyColorColorizer1.PredefinedColorSchema = DevExpress.XtraMap.PredefinedColorSchema.Palette;
            this.FileLayer.Colorizer = keyColorColorizer1;
            this.FileLayer.Data = this.ShapefileDataAdapter;
            this.FileLayer.Name = "FileLayer";
            this.FileLayer.DataLoaded += new DevExpress.XtraMap.DataLoadedEventHandler(this.LayerDataLoaded);
            this.ShapefileDataAdapter.ItemsLoaded += new DevExpress.XtraMap.ItemsLoadedEventHandler(this.ShapeItemsLoaded);
            // 
            // mapControl1
            // 
            this.mapControl1.BackColor = System.Drawing.Color.LightBlue;
            this.mapControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mapControl1.CenterPoint = new DevExpress.XtraMap.GeoPoint(33D, 0D);
            geoMapCoordinateSystem1.Projection = millerProjection1;
            this.mapControl1.CoordinateSystem = geoMapCoordinateSystem1;
            this.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl1.Layers.Add(this.FileLayer);
            this.mapControl1.Location = new System.Drawing.Point(0, 0);
            this.mapControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.mapControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mapControl1.MaxZoomLevel = 8D;
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.NavigationPanelOptions.Visible = false;
            this.mapControl1.SelectionMode = DevExpress.XtraMap.ElementSelectionMode.Single;
            this.mapControl1.Size = new System.Drawing.Size(331, 341);
            this.mapControl1.TabIndex = 0;
            this.mapControl1.ZoomLevel = 2D;
            this.mapControl1.OverlaysArranged += new DevExpress.XtraMap.OverlaysArrangedEventHandler(this.MapControl_OverlaysArranged);
            this.mapControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapControl1_MouseUp);
            // 
            // FlagGamePage
            // 
            this.Appearance.BackColor = System.Drawing.Color.LightBlue;
            this.Appearance.Options.UseBackColor = true;
            this.Controls.Add(this.mapControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FlagGamePage";
            this.Size = new System.Drawing.Size(331, 341);
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MapControl mapControl1;
        private VectorItemsLayer FileLayer;
        private ShapefileDataAdapter ShapefileDataAdapter;
    }
}
