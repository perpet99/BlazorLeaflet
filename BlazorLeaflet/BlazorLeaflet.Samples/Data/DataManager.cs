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

            if (15 < index)
                return result;
            //index = 9;


            int cx = (int)center.Lng*1000000;
            int cy = (int)center.Lat*1000000;
            int segx;
            int segy;
            switch (index)
            {
                case 1:segx = 
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;

                default:
                    break;
            }


            
            
            //경계선 넘김처리

            for (int x = center.Lng - 3; x < xindex + 3; x++)
            {
                for (int  y = yindex - 3; y < yindex + 3; y++)
                {
                    var key = $"{index},{x},{y}";

                    if (_KeyList.TryGetValue(key, out List<CamFileInfo> list))
                    {
                        // 루트면 리스트 추가
                        if(index == 1)
                        {
                            result.AddRange( list );
                        }else // 아니면 대표 아이콘 추가
                        {
                            var newItem = new CamFileInfo();
                            var lng = x * index  <= 180 ? x * index : -360 + x * index;
                            var lat =  y * index <= 90 ? y * index : 180 -(x * index);
                            lat = -90 < lat ? lat : -180 - lat;

                            newItem.AddLatLng( new LatLng( lat,lng));
                            result.Add(newItem);
                        }
                    }
                    else
                    {
                        var newItem = new CamFileInfo();
                        var lng = x * index <= 180 ? x * index : -360 + x * index;
                        var lat = y * index <= 90 ? y * index : 180 - (x * index);
                        lat = -90 < lat ? lat : -180 - lat;

                        newItem.AddLatLng(new LatLng(lat, lng));
                        result.Add(newItem);

                    }

                }

            }
            return result;
        }

        private void AddOrUpdateIndex(int i, CamFileInfo item)
        {
            //double div = Math.Pow((double)10,(double) i);
            int xindex = (int)item._LatLngs[0].Lng / i;
            int yindex = (int)item._LatLngs[0].Lat / i;

            var key = $"{i},{xindex},{yindex}";

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
