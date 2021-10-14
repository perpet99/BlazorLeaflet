using BlazorLeaflet.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorLeaflet.Samples.Data
{

    public class CamFileInfo
    {
        internal void AddLatLng(LatLng latLng)
        {
            _LatLngs.Add(latLng);
        }

        public List<LatLng> _LatLngs= new List<LatLng>();

        public DateTime Date { get; internal set; }
    }

    public class PosInfos : List<LatLng>
    { 
        
    }



    public class DataManager
    {
        public static DataManager I = new DataManager();

        


        //void RecusiveAddKey(string key,int count)
        //{
        //    if (count-- == 0)
        //        return;

        //    for (int i = 1; i <= 4; i++)
        //    {
        //        _KeyList.Add(key + i.ToString(), 0);
        //        RecusiveAddKey(key + i, count);
        //    }

        //}

        internal void Init()
        {
            //throw new NotImplementedException();

            // make grid
            //RecusiveAddKey("",24);
        }

        public bool Contains(DateTime date)
        {
            foreach (var item in _CamFileInfosWithPositon)
            {
                if( date.Year == item.Date.Year 
                    &&  date.Month == item.Date.Month
                    && date.Day == item.Date.Day)
                    return true;
            }

            return false;
        }

        internal List<CamFileInfo> LoadFile()
        {
            return new List<CamFileInfo>();
        }

        public List<CamFileInfo> _CamFileInfosWithPositon = new List<CamFileInfo>();
        public List<CamFileInfo> _CamFileInfosWithNoPositon = new List<CamFileInfo>();

        internal void SaveFile(List<CamFileInfo> flist)
        {
            foreach (var item in flist)
            {
                if( item._LatLngs.Count() == 0)
                {
                    _CamFileInfosWithNoPositon.Add(item);
                }
                else
                {
                    _CamFileInfosWithPositon.Add(item);

                    int zoom = 24;

                    TileMath.PositionToTileXY(new double[] { item._LatLngs[0].Lng, item._LatLngs[0].Lat }, 24, 256, out int x, out int y);

                    var key = TileMath.TileXYToQuadKey(x, y, zoom);




                }
            }
        }

        internal PosInfos GetPosList(int zoom)
        {
            var _PosInfos  = new PosInfos();
            foreach (var item in _CamFileInfosWithPositon)
            {
                _PosInfos.Add(item._LatLngs[0]);
            }

            return _PosInfos;
        }

        Dictionary<string, List<CamFileInfo>> _KeyList = new Dictionary<string, List<CamFileInfo>>();

        internal void UpdateIndex(List<CamFileInfo> list)
        {
            for (int i = 1; i <= 9; i++)
            {
                foreach (var item in list)
                {
                    AddOrUpdateIndex(i, item);
                }
                
            }
        }


        
        public List<CamFileInfo> GetListByLevel(int index,LatLng center)
        {
            List<CamFileInfo> result = new List<CamFileInfo>();

            if (index < 6 )
                return result;
            //index = 9;
            center.Lng = center.Lng % 180;
            index = 19 - index;
            GetsegmentIndex(index, center, out int ix, out int iy, out int sx, out int sy);




            //경계선 넘김처리

            for (int x = ix - 4; x < ix + 8; x++)
            {
                for (int  y = iy - 6; y < iy + 6; y++)
                {
                    var key = $"{index},{x},{y}";

                    if (_KeyList.TryGetValue(key, out List<CamFileInfo> list))
                    {
                        // 루트면 리스트 추가
                        if(index <= 4)
                        {
                            result.AddRange( list );
                        }else // 아니면 대표 아이콘 추가
                        {
                            var newItem = new CamFileInfo();
                            var latlng = GetLatLngFromIndex(x,y,sx,sy);

                            newItem.AddLatLng(latlng);
                            result.Add(newItem);
                        }
                    }
                    else
                    {
                        //var newItem = new CamFileInfo();
                        //var latlng = GetLatLngFromIndex(x, y, sx, sy);

                        //newItem.AddLatLng(latlng);
                        //result.Add(newItem);

                    }

                }

            }
            return result;
        }

        LatLng GetLatLngFromIndex(int x,int y,int sx,int sy)
        {
            var latlng = new LatLng(y / shift, x / shift);
            var newItem = new CamFileInfo();
            latlng.Lng = latlng.Lng * sx <= 180 ? latlng.Lng * sx : -360 + latlng.Lng * sx;
            latlng.Lat = latlng.Lat * sy <= 90 ? latlng.Lat * sy : 180 - (latlng.Lat * sy);
            latlng.Lat = -90 < latlng.Lat ? latlng.Lat : -180 - latlng.Lat;

            return latlng;
        }

        void Getsegment(int index,out int sx,out int sy)
        {
            //sx = (int)Math.Pow(4, index);
            //sy = (int)Math.Pow(4, index);

            switch (index)
            {
                case 1:
                    sx = 450; sy = 450;
                    break;
                case 2:
                    sx = 900; sy = 900;
                    break;
                case 3:
                    sx = 1800; sy = 1800;
                    break;
                case 4:
                    sx = 3500; sy = 3500;
                    break;
                case 5:
                    sx = 7000; sy = 7000;
                    break;
                case 6:
                    sx = 14000; sy = 14000;
                    break;
                case 7:
                    sx = 28000; sy = 28000;
                    break;
                case 8:
                    sx = 56000; sy = 56000;
                    break;
                case 9:
                    sx = 118000; sy = 118000;
                    break;
                case 10:
                    sx = 204000; sy = 204000;
                    break;
                case 11:
                    sx = 480000; sy = 480000;
                    break;
                case 12:
                    sx = 840000; sy = 840000;
                    break;
                case 13:
                    sx = 1800000; sy = 1800000;
                    break;
                case 14:
                    sx = 5000000; sy = 5000000;
                    break;
                    
                default:
                    sx = 9000000; sy = 9000000;
                    break;
            }
            sx /= 2;
            sy /= 2;
        }


        void GetsegmentIndex(int index, LatLng latlng, out int ix, out int iy)
        {
            GetsegmentIndex( index,  latlng,out ix,out iy,out var sx,out var sy);
        }
        const float shift = 1000000;

        void GetsegmentIndex(int index, LatLng latlng, out int ix, out int iy, out int sx, out int sy)
        {
            Getsegment(index,out sx,out sy);

            ix = (int)(latlng.Lng * shift) /sy;
            iy = (int)(latlng.Lat * shift) /sx;
        }


        private void AddOrUpdateIndex(int i, CamFileInfo item)
        {

            GetsegmentIndex(i, item._LatLngs[0],out var sx, out var sy);


            var key = $"{i},{sx},{sy}";

            if(_KeyList.TryGetValue(key, out List<CamFileInfo> list) ==false)
            {
                list = new List<CamFileInfo>();
                _KeyList.Add(key, list);
            }
            list.Add(item);

        }

        internal List<CamFileInfo> GetListByDate(DateTime date)
        {
            var list = new List<CamFileInfo>();

            foreach (var item in _CamFileInfosWithPositon)
            {
                if (date.Year == item.Date.Year
                    && date.Month == item.Date.Month
                    && date.Day == item.Date.Day)
                {
                    list.Add(item);
                    //if( list.Contains(item.Date.ToString()) == false)
                    //    list.Add(item.Date.ToString());
                }
                    
            }

            return list;
        }
    }
}
