using System.Threading;
using System.Linq;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using BlazorLeaflet.Models;

using System.IO;
using BlazorLeaflet.Samples.Data;

namespace BlazorLeaflet.Samples.Pages
{
    public partial class Index
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public string LiveResolution { get; set; }

        [Inject]
        IJSRuntime jsRuntime { get; set; }

        [Inject]
        CityService cityService { get; set; }


        private Map _map;

        private Circle _circle;


        void dbtest()
        {
            using (var db = new  Models.teslamateContext())
            {
                var end = DateTime.Now;
                var start = end.AddDays(-30);

                var r = db.Positions.Where(item => start < item.Date && item.Date < end );


                Console.WriteLine(r.Count().ToString());

                foreach (var item in r)
                {
                    
                }
                //파일을 읽어들이고
                //날짜 기준으로 정리하고 
                //해당 날짜기준 1분 경로를 링크를 걸고 저장

                //



                //// Creating a new item and saving it to the database
                //var newItem = new Item();
                //newItem.Name = "Red Apple";
                //newItem.Description = "Description of red apple";
                //db.Item.Add(newItem);
                //var count = db.SaveChanges();
                //Console.WriteLine("{0} records saved to database", count);
                //// Retrieving and displaying data
                //Console.WriteLine();
                //Console.WriteLine("All items in the database:");
                //foreach (var item in db.Item)
                //{
                //    Console.WriteLine("{0} | {1}", item.Name, item.Description);
                //}
            }
        }

        protected override void OnInitialized()
        {

            dbtest();

            _map = new Map(jsRuntime)
            {
                Center = _startAt,
                Zoom = 4.8f
            };

            _map.OnInitialized += () =>
            {
                _map.AddLayer(new TileLayer
                {
                    UrlTemplate = "https://a.tile.openstreetmap.org/{z}/{x}/{y}.png",
                    Attribution = "&copy; <a href=\"https://www.openstreetmap.org/copyright\">OpenStreetMap</a> contributors",
                });

                _map.AddLayer(new Polygon
                {
                    Shape = new[]
                    { new[] { new PointF(37f, -109.05f), new PointF(41f, -109.03f), new PointF(41f, -102.05f), new PointF(37f, -102.04f) } },
                    Fill = true,
                    FillColor = Color.Blue,
                    Popup = new Popup
                    {
                        Content = "How are you doing,"
                    }
                });

                _map.AddLayer(new BlazorLeaflet.Models.Rectangle
                {
                    Shape = new RectangleF(10f, 0f, 5f, 1f)
                });

                _circle = new Circle
                {
                    Position = new LatLng(10f, 5f),
                    Radius = 10f
                };
                _map.AddLayer(_circle);
            };
        }

        private LatLng _startAt = new LatLng(47.5574007f, 16.3918687f);
        public string CityName { get; set; }


        private void FindCity()
        {
            _circle.Radius = 5000000f;
            var city = cityService.FindCity(CityName);
            if (city != null)
            {
                var marker = new Marker(city.Coordinates)
                {
                    Icon = new Icon
                    {
                        Url = city.CoatOfArmsImageUrl,
                        ClassName = "map-icon",
                    },
                    Tooltip = new Tooltip
                    {
                        Content = city.Name,
                    },
                    Popup = new Popup
                    {
                        Content = city.Description,
                    }
                };

                _map.AddLayer(marker);
            }
        }

        private void ZoomMap()
        {
            _map.FitBounds(new PointF(45.943f, 24.967f), new PointF(46.943f, 25.967f), maxZoom: 5f);
        }

        private void PanToNY()
        {
            _map.PanTo(new PointF(40.713185f, -74.0072333f), animate: true, duration: 10f);
        }
    }
}

