using FireSharp.Response;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CargoTrackingSystem
{
    public partial class MapPage : Form
    {
        public MapPage()
        {
            InitializeComponent();
        }


        private void zamanlayaci(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            rotaHesapla();
        }


        private void MapPage_Load(object sender, EventArgs e)
        {

            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gMapControl1.CacheLocation = @"cache";
            gMapControl1.SetPositionByKeywords("Türkiye");
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.ShowCenter = false;
            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 7;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = (5 * 1000);
            timer.Tick += new EventHandler(zamanlayaci);
            timer.Start();

        }


        void rotaHesapla()
        {
            GMapProviders.GoogleMap.ApiKey = ApiKey.Key;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.MaxZoom = 100;
            gMapControl1.MinZoom = 1;
            gMapControl1.Zoom = 10;

            for (int i = 0; i < GlobalArray.globalNoktalar.Count; i++)
            {
                if (i == 0)
                {
                    double lat = Convert.ToDouble(GlobalArray.globalNoktalar[Convert.ToInt32(GlobalArray.toplamKenar[GlobalArray.index, i])].Lat);
                    double lng = Convert.ToDouble(GlobalArray.globalNoktalar[Convert.ToInt32(GlobalArray.toplamKenar[GlobalArray.index, i])].Lng);
                    gMapControl1.Position = new PointLatLng(lat, lng);
                    PointLatLng point = new PointLatLng(lat, lng);
                    GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.blue);
                    GMapOverlay markers = new GMapOverlay("markers");
                    markers.Markers.Add(marker);
                    gMapControl1.Overlays.Add(markers);
                }
                else
                {
                    double lat = Convert.ToDouble(GlobalArray.globalNoktalar[Convert.ToInt32(GlobalArray.toplamKenar[GlobalArray.index, i])].Lat);
                    double lng = Convert.ToDouble(GlobalArray.globalNoktalar[Convert.ToInt32(GlobalArray.toplamKenar[GlobalArray.index, i])].Lng);
                    gMapControl1.Position = new PointLatLng(lat, lng);
                    PointLatLng point = new PointLatLng(lat, lng);
                    GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);
                    GMapOverlay markers = new GMapOverlay("markers");
                    markers.Markers.Add(marker);
                    gMapControl1.Overlays.Add(markers);
                }
                if (i + 1 < GlobalArray.globalNoktalar.Count)
                {
                    Console.WriteLine(Convert.ToInt32(GlobalArray.toplamKenar[GlobalArray.index, i]).ToString() + Convert.ToInt32(GlobalArray.toplamKenar[GlobalArray.index, i + 1]).ToString());

                    var route = GoogleMapProvider.Instance.GetRoute(GlobalArray.globalNoktalar[Convert.ToInt32(GlobalArray.toplamKenar[GlobalArray.index, i])], GlobalArray.globalNoktalar[Convert.ToInt32(GlobalArray.toplamKenar[GlobalArray.index, i + 1])], false, false, 14);
                    var r = new GMapRoute(route.Points, "My Route")
                    {
                        Stroke = new Pen(Color.Red, 5)
                    };

                    var routes = new GMapOverlay("routes");
                    routes.Routes.Add(r);
                    gMapControl1.Overlays.Add(routes);

                }

            }

        }

        private void gMap_Load(object sender, EventArgs e)
        {

        }
    }
}