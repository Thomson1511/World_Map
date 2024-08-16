using GMap.NET.MapProviders;
using GMap.NET;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace Ujterkep
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Térkép beállítása
            gMapControl1.MapProvider = GMapProviders.OpenStreetMap; // Választható másik szolgáltató is
            gMapControl1.Position = new PointLatLng(47.4979, 19.0402); // Kezdő koordináták (Budapest)
            gMapControl1.MinZoom = 2;
            gMapControl1.MaxZoom = 18;
            gMapControl1.Zoom = 6;

            // Interakciók engedélyezése
            gMapControl1.CanDragMap = true;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.ShowCenter = false;
            gMapControl1.IgnoreMarkerOnMouseWheel = true;

            // Több pont összekötése
            var LHCC_FIR_Boundary = new List<PointLatLng>
            {
                new PointLatLng(ConvertDMSToDecimal("N48 00 24"), ConvertDMSToDecimal("E17 09 39")),
                new PointLatLng(ConvertDMSToDecimal("N47 58 17"), ConvertDMSToDecimal("E17 05 50")),
                new PointLatLng(ConvertDMSToDecimal("N47 55 00"), ConvertDMSToDecimal("E17 06 00")),
                new PointLatLng(ConvertDMSToDecimal("N47 52 29"), ConvertDMSToDecimal("E17 05 11")),
                new PointLatLng(ConvertDMSToDecimal("N47 52 06"), ConvertDMSToDecimal("E17 01 20")),
                new PointLatLng(ConvertDMSToDecimal("N47 48 20"), ConvertDMSToDecimal("E17 04 00")),
                new PointLatLng(ConvertDMSToDecimal("N47 46 00"), ConvertDMSToDecimal("E17 04 00")),
                new PointLatLng(ConvertDMSToDecimal("N47 42 27"), ConvertDMSToDecimal("E17 05 00")),
                new PointLatLng(ConvertDMSToDecimal("N47 41 20"), ConvertDMSToDecimal("E16 54 50")),
                new PointLatLng(ConvertDMSToDecimal("N47 41 25"), ConvertDMSToDecimal("E16 52 22"))
            };

            var Lesmo_Area = new List<PointLatLng>
            {
                new PointLatLng(ConvertDMSToDecimal("N48 00 24"), ConvertDMSToDecimal("E017 09 39")),
                new PointLatLng(ConvertDMSToDecimal("N47 49 06"), ConvertDMSToDecimal("E017 36 51")),
                new PointLatLng(ConvertDMSToDecimal("N47 44 49"), ConvertDMSToDecimal("E017 30 00")),
                new PointLatLng(ConvertDMSToDecimal("N47 35 59"), ConvertDMSToDecimal("E017 29 18")),
                new PointLatLng(ConvertDMSToDecimal("N47 35 59"), ConvertDMSToDecimal("E017 15 54")),
                new PointLatLng(ConvertDMSToDecimal("N47 35 55"), ConvertDMSToDecimal("E016 40 05")),
                new PointLatLng(ConvertDMSToDecimal("N48 00 24"), ConvertDMSToDecimal("E017 09 39")),
            };
            var Sopro_Line = new List<PointLatLng>
            {
                new PointLatLng(48.0024, 17.0939),
                new PointLatLng(47.4227, 17.0500),
                new PointLatLng(47.3244, 16.4214),
                new PointLatLng(46.5952, 16.1329),
                new PointLatLng(46.5046, 16.2019)
            };

            var points = new List<(string Name, PointLatLng Location)>
            {
                ("NATEX", new PointLatLng(ConvertDMSToDecimal("N47 44 49"), ConvertDMSToDecimal("E017 30 00"))),
                ("KUVEX", new PointLatLng(ConvertDMSToDecimal("N47 54 30"), ConvertDMSToDecimal("E017 26 15"))),
                ("GASNA", new PointLatLng(ConvertDMSToDecimal("N47 53 59"), ConvertDMSToDecimal("E017 07 59"))),
                ("BEGLA", new PointLatLng(ConvertDMSToDecimal("N47 49 51"), ConvertDMSToDecimal("E017 06 52"))),
                ("PESAT", new PointLatLng(ConvertDMSToDecimal("N47 42 54"), ConvertDMSToDecimal("E017 03 11"))), 
                ("ABETI", new PointLatLng(ConvertDMSToDecimal("N47 40 40"), ConvertDMSToDecimal("E017 00 46"))), 
                ("SOPRO", new PointLatLng(ConvertDMSToDecimal("N47 35 16"), ConvertDMSToDecimal("E016 48 09"))), 
                ("ARSIN", new PointLatLng(ConvertDMSToDecimal("N47 34 02"), ConvertDMSToDecimal("E016 45 13"))),  
                ("STEIN", new PointLatLng(ConvertDMSToDecimal("N47 25 39"), ConvertDMSToDecimal("E016 35 59"))),
                ("SASAL", new PointLatLng(ConvertDMSToDecimal("N47 17 05"), ConvertDMSToDecimal("E016 28 28"))),
                ("SUNIS", new PointLatLng(ConvertDMSToDecimal("N47 08 31"), ConvertDMSToDecimal("E016 20 59"))),
                ("GOTAR", new PointLatLng(ConvertDMSToDecimal("N46 59 52"), ConvertDMSToDecimal("E016 13 29"))),
                ("DIMLO", new PointLatLng(ConvertDMSToDecimal("N46 41 01"), ConvertDMSToDecimal("E016 25 22")))
            };

            // colors
            Color Fir_Color = Color.Red;
            Color Lesmo = Color.Blue;

            AddRouteBetweenPoints(LHCC_FIR_Boundary, Fir_Color);
            AddRouteBetweenPoints(Lesmo_Area, Lesmo);
            AddMarkersAtPoints(points);
        }

        private double ConvertDMSToDecimal(string dms)
        {
            // Az első karakter az irány (N, S, E, W)
            char direction = dms[0];

            // A karakterlánc többi része a számértékeket tartalmazza
            string[] parts = dms.Substring(1).Split(' ');

            int degrees = int.Parse(parts[0]);
            int minutes = int.Parse(parts[1]);
            double seconds = double.Parse(parts[2]);

            // Konverzió tizedes fokokká
            double decimalDegrees = degrees + (minutes / 60.0) + (seconds / 3600.0);

            // Ha a déli (S) vagy nyugati (W) féltekén van, negatívvá tesszük az értéket
            if (direction == 'S' || direction == 'W')
            {
                decimalDegrees *= -1;
            }

            return decimalDegrees;
        }

        private void AddRouteBetweenPoints(List<PointLatLng> points, Color penColor)
        {
            if (points == null || points.Count < 2)
            {
                MessageBox.Show("Legalább két pont szükséges az útvonalhoz.");
                return;
            }

            // Új réteg létrehozása az útvonalakhoz
            var routesOverlay = new GMap.NET.WindowsForms.GMapOverlay("routes");

            // Útvonal létrehozása a megadott pontok alapján
            var route = new GMap.NET.WindowsForms.GMapRoute(points, "My Route")
            {
                Stroke = new Pen(penColor, 3) // Fekete vonal 3 pixel szélességgel
            };

            // Korábbi rétegek törlése NEM kell
            //gMapControl1.Overlays.Clear();

            // Útvonal hozzáadása a réteghez
            routesOverlay.Routes.Add(route);

            // Réteg hozzáadása a térképhez
            gMapControl1.Overlays.Add(routesOverlay);

            // Térkép frissítése az útvonal megjelenítéséhez
            gMapControl1.Refresh();
        }

        private void AddMarkersAtPoints(List<(string Name, PointLatLng Location)> points)
        {
            // Új overlay létrehozása a marker-ekhez
            var markersOverlay = new GMap.NET.WindowsForms.GMapOverlay("markers");

            // Elérési út a zöld háromszög ikonhoz
            string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "green_triangle.png");

            if (!File.Exists(iconPath))
            {
                MessageBox.Show("A green_triangle.png ikon nem található a program könyvtárában.");
                return;
            }

            // Betöltjük az ikont
            Bitmap bitmap = new Bitmap(iconPath);

            // Végigmegyünk a pontokon, és mindegyikhez hozzáadunk egy markert és egy címkét
            foreach (var point in points)
            {
                // Egyedi marker létrehozása
                var marker = new CustomMarker(point.Location, bitmap, point.Name);

                // Marker hozzáadása az overlay-hez
                markersOverlay.Markers.Add(marker);
            }

            // Hozzáadjuk a marker overlay-t a térképhez
            gMapControl1.Overlays.Add(markersOverlay);

            // Térkép frissítése
            gMapControl1.Refresh();
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var point = gMapControl1.FromLocalToLatLng(e.X, e.Y);
                MessageBox.Show($"Latitude: {point.Lat}, Longitude: {point.Lng}");
            }
        }
    }
    // Egyedi marker osztály
    public class CustomMarker : GMapMarker
    {
        private readonly Bitmap icon;
        private readonly string tooltipText;

        public CustomMarker(PointLatLng p, Bitmap icon, string tooltipText)
            : base(p)
        {
            this.icon = icon;
            this.tooltipText = tooltipText;
            Size = new Size(icon.Width, icon.Height);
        }

        public override void OnRender(Graphics g)
        {
            g.DrawImage(icon, LocalPosition.X - Size.Width / 2, LocalPosition.Y - Size.Height / 2, Size.Width, Size.Height);
        }

        public override void Dispose()
        {
            icon.Dispose();
            base.Dispose();
        }
    }
}
