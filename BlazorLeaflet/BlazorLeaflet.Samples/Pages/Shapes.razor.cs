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

namespace BlazorLeaflet.Samples.Pages
{
    public partial class Shapes
    {
        private Map _map;
        private DrawHandler _drawHandler;
        private LatLng _markerLatLng = new LatLng { Lat = 47.5574007f, Lng = 16.3918687f };

        //Marker _marker;

        public class MarkerInfo
        { 
            public Marker marker;
        }


        List<MarkerInfo> _MarkerList = new List<MarkerInfo>();

        List<Polyline> _polyLines = new List<Polyline>();

        protected override void OnInitialized()
        {
           

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

               
            };

            _map.OnClick += _map_OnClick;

            _drawHandler = new DrawHandler(_map, jsRuntime);

            
        }

        private void _map_OnClick(Map sender, MouseEvent e)
        {
            AddMarket(e.LatLng);
            var to = e.LatLng;
            to.Lat += 1;

            var p = new Polyline();

            p.StrokeColor = Color.Red;


            var shape = new PointF[1][];
            shape[0] = new PointF[3];

            shape[0][0] = e.LatLng.ToPointF();
            e.LatLng.Lat += 1;
            shape[0][1] = e.LatLng.ToPointF();
            e.LatLng.Lng += 1;
            shape[0][2] = e.LatLng.ToPointF();
            
            p.Shape = shape;

            
            _map.AddLayer(p);

            _polyLines.Add(p);

            //_drawHandler.AddPolyLine(e.LatLng, to);
        }

        private MarkerInfo AddMarket(LatLng latLng)
        {

            var mi = new MarkerInfo();

            mi.marker = new Marker(latLng)
            {
                Draggable = true,
                Title = "Marker 1",
                Popup = new Popup { Content = string.Format("I am at {0:0.00}° lat, {1:0.00}° lng", latLng.Lat, latLng.Lng) },
                Tooltip = new BlazorLeaflet.Models.Tooltip { Content = "Click and drag to move me" }
            };

            mi.marker.OnMove += OnDrag;
            mi.marker.OnMoveEnd += OnDragEnd;
            mi.marker.OnClick += Marker_OnClick;

            _map.AddLayer(mi.marker);

            _MarkerList.Add(mi);

            return mi;
        }

        private void Marker_OnClick(InteractiveLayer sender, MouseEvent e)
        {
            var marker = _MarkerList.FirstOrDefault(item => item.marker == sender);

            if (marker == null) return;

            
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
            //if (ForecastService.GetRecordFileInfoByDate(args.Date) != null)
            {
                args.Attributes.Add("style", "background-color: #ff6d41; border-color: white;");
            }
        }

        void RadzenDatePickerOnChange(DateTime? value, string name, string format)
        {


            //timeValue = value.Value;
            //_dayinfoList = recordFileInfos.GetTimeRecordFile24(value.Value);

            //ListBoxItems.Clear();

            //ListBoxItems = _dayinfoList.Select(item => item.index.ToString()).ToList();

        }

        string ListBoxValue;
        List<string> ListBoxItems = new List<string>();


        async Task ListBoxOnChange(object value, string message)
        {
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

    }
}

