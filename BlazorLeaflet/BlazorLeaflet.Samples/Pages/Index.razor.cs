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
using BlazorLeaflet.Models.Events;
using Radzen;
using System.Reflection;

namespace BlazorLeaflet.Samples.Pages
{
    public partial class Index
    {
        private Map _map;
        private DrawHandler _drawHandler;
        private LatLng _markerLatLng = new LatLng { Lat = 47.5574007f, Lng = 16.3918687f };

        //Marker _marker;

        public class MarkerInfo
        {
            public Marker marker;
        }

        [Inject]
        IJSRuntime jsRuntime { get; set; }

        List<MarkerInfo> _MarkerList = new List<MarkerInfo>();

        List<Polyline> _polyLines = new List<Polyline>();

        void dbtest()
        {

            DataManager.I.Init();


            // 파일로드해서 최신 정보있는지 확인한다.
            var flist = DataManager.I.LoadFile();

            for (int i = 0; i < 5; i++)
            {
                var c = new CamFileInfo();
                c.Date = new DateTime(2021, 10, 10, 12, i, 0);

                flist.Add(c);

            }

            using (var db = new Models.teslamateContext())
            {

                foreach (var item in flist)
                {
                    var end = item.Date;

                    var start = end.AddMinutes(-10);

                    var r = db.Positions.Where(item => start < item.Date && item.Date < end).OrderByDescending(item => item.Date).Take(10);

                    if (r.Count() == 0)
                    {
                        break;
                    }

                    int count = 0;
                    foreach (var item2 in r)
                    {
                        LatLng latLng = new LatLng();
                        latLng.Lat = (float)item2.Latitude;
                        latLng.Lng = (float)item2.Longitude;

                        item.AddLatLng(latLng);
                        if (++count == 10)
                        {
                            break;
                        }

                    }

                    //Console.WriteLine(r.Count().ToString());


                }

                DataManager.I.SaveFile(flist);

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
                Center = _markerLatLng,
                Zoom = 4.8f
            };

            _map.OnInitialized += () =>
            {
                _map.AddLayer(new TileLayer
                {
                    UrlTemplate = "https://a.tile.openstreetmap.org/{z}/{x}/{y}.png",
                    Attribution = "&copy; <a href=\"https://www.openstreetmap.org/copyright\">OpenStreetMap</a> contributors",
                });

                var list = DataManager.I.GetPosList(0);

                foreach (var item in list)
                {
                    //AddMarket(item);
                }


                foreach (var item in DataManager.I._CamFileInfosWithPositon)
                {
                    
                }

            };

            _map.OnClick += _map_OnClick;
            _map.OnZoomLevelsChange += _map_OnZoomLevelsChange;
            _map.OnZoomEnd += _map_OnZoomEnd;
            _drawHandler = new DrawHandler(_map, jsRuntime);


        }

        
        //줌 단계별로 아이콘 보이기
        private async void _map_OnZoomEnd(object sender, Event e)
        {
            var r = await _map.GetZoom();
            


            if( 20 < r)
            {

            }else if( 18 < r)
            {

            }
            else if (14 < r)
            {

            }
            else if (10 < r)
            {

            }
            else if (5 < r)
            {

            }
            else 
            {

            }


            Console.WriteLine(r.ToString());

            Console.WriteLine(_map.Zoom.ToString());
        }

        private void _map_OnZoomLevelsChange(object sender, Event e)
        {
            Console.WriteLine(_map.Zoom.ToString());
        }

        private void AddLine(List<LatLng> latLngs)
        {
            var p = new Polyline();

            p.StrokeColor = Color.Red;


            var shape = new PointF[1][];
            shape[0] = new PointF[latLngs.Count()];

            for (int i = 0; i < latLngs.Count(); i++)
            {
                shape[0][i] = latLngs[i].ToPointF();
            }

            p.Shape = shape;

            _map.AddLayer(p);
        }

        private void _map_OnClick(Map sender, MouseEvent e)
        {
            //AddMarket(e.LatLng);
            //var to = e.LatLng;
            //to.Lat += 1;

            //var p = new Polyline();

            //p.StrokeColor = Color.Red;


            //var shape = new PointF[1][];
            //shape[0] = new PointF[3];

            //shape[0][0] = e.LatLng.ToPointF();
            //e.LatLng.Lat += 1;
            //shape[0][1] = e.LatLng.ToPointF();
            //e.LatLng.Lng += 1;
            //shape[0][2] = e.LatLng.ToPointF();

            //p.Shape = shape;


            //_map.AddLayer(p);

            //_polyLines.Add(p);



            //_drawHandler.AddPolyLine(e.LatLng, to);
        }


        //https://github.com/pointhi/leaflet-color-markers

        private MarkerInfo AddMarker(CamFileInfo item ,bool colorBlue = true)
        {
            var title = item.Date.ToString();

            LatLng latLng = item._LatLngs[0];


            var mi = new MarkerInfo();

            mi.marker = new Marker(latLng)
            {
                Draggable = false,
                Title = "Marker 1",
                //Popup = new Popup { Content = title},
                Tooltip = new BlazorLeaflet.Models.Tooltip { Content = title }
                
            };
            
            if(colorBlue)
                mi.marker.Icon = new BlazorLeaflet.Models.Icon { Url = "/img/marker-icon-blue.png", Anchor = new Point(13,41) };
            else
                mi.marker.Icon = new BlazorLeaflet.Models.Icon { Url = "/img/marker-icon-red.png", Anchor = new Point(13, 41) };

            mi.marker.OnMove += OnDrag;
            mi.marker.OnMoveEnd += OnDragEnd;
            mi.marker.OnClick += Marker_OnClick;

            _map.AddLayer(mi.marker);

            _MarkerList.Add(mi);

            return mi;
        }

        DayItemInfo CurSel = null;

        private void Marker_OnClick(InteractiveLayer sender, MouseEvent e)
        {
            var sel = _days.FirstOrDefault(item => item.Marker.marker == sender);

            if (sel == null) return;

            ListBoxValue = sel.Item.Date.ToString();

            SelectItem(sel);
           
        }

        private void SelectItem(DayItemInfo sel)
        {
            _map.RemoveLayer(sel.Marker.marker);

            AddMarker(sel.Item, false);

            if (CurSel != null)
            {
                _map.RemoveLayer(CurSel.Marker.marker);
                AddMarker(CurSel.Item);
            }

            CurSel = sel;

            StateHasChanged();
        }

        async Task ListBoxOnChange(object value, string message)
{

            var sel = _days.FirstOrDefault(item => item.Item.Date.ToString() == value.ToString());

            if (sel == null) return;


            SelectItem(sel);

            //SelectDate = DateTime.Parse(value.ToString());

            //var f = _dayinfoList.FirstOrDefault(item => item.index == SelectDate);
            //if (f == null)
            //    return;

            //TimeRecordFiles = f.FileList;

            //await StartPlay(0, true);



            //await jsRuntime.InvokeVoidAsync("tttt", "JS function called from .NET");
            //await jsRuntime.InvokeVoidAsync("loadVideo", "id1");
            //StateHasChanged();

        }

        private void OnDrag(Marker marker, DragEvent evt)
        {
            _markerLatLng = evt.LatLng;
            StateHasChanged();
        }

        private async void OnDragEnd(Marker marker, Event e)
        {
            marker.Position = _markerLatLng;
            marker.Popup.Content = string.Format("I am now at {0:0.00}° lat, {1:0.00}° lng", _markerLatLng.Lat, _markerLatLng.Lng);
            await LeafletInterops.UpdatePopupContent(jsRuntime, _map.Id, marker);
        }


        DateTime RadzenDatePickerValue = DateTime.Now;

        void DateRenderSpecial(DateRenderEventArgs args)
        {
            if (DataManager.I.Contains(args.Date) )
            {
                args.Attributes.Add("style", "background-color: #ff6d41; border-color: white;");
            }
        }

        public class DayItemInfo
        {
            public MarkerInfo Marker { get; internal set; }
            public CamFileInfo Item { get; internal set; }
        }

        List<DayItemInfo> _days = new List<DayItemInfo>();

        int _CurLOD = -1;

        void UpdateMarker(int lod)
        {
            if (_CurLOD == lod)
                return;
            _map.ClearLayer();


        }

        void RadzenDatePickerOnChange(DateTime? value, string name, string format)
        {


            //timeValue = value.Value;
            //_dayinfoList = recordFileInfos.GetTimeRecordFile24(value.Value);

            ListBoxItems.Clear();
            
            var list = DataManager.I.GetListByDate(value.Value);


            foreach (var item in list)
            {
                if (ListBoxItems.Contains(item.Date.ToString()))
                    continue;

                var di = new DayItemInfo();
                _days.Add(di);

                ListBoxItems.Add(item.Date.ToString());
                di.Item = item;
                di.Marker = AddMarker(item);

                AddLine(item._LatLngs);
            }

            if(0 < list.Count )
            {
                
                //_map.PanTo(new PointF(list[0]._LatLngs[0].Lat, list[0]._LatLngs[0].Lng), animate: true, duration: 2f);

                _map.FitBounds(new PointF(list[0]._LatLngs[0].Lat, list[0]._LatLngs[0].Lng), new PointF(list[0]._LatLngs[0].Lat, list[0]._LatLngs[0].Lng), maxZoom: 12f);


            }



        }

        string ListBoxValue;
        List<string> ListBoxItems = new List<string>();



    }
}

