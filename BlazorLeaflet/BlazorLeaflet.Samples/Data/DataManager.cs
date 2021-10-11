using BlazorLeaflet.Models;
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

        internal void Init()
        {
            //throw new NotImplementedException();
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
