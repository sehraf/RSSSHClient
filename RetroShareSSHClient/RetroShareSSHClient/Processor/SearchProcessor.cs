using System;
using System.Collections.Generic;

using rsctrl.core;
using rsctrl.search;

namespace RetroShareSSHClient
{
    struct GuiSearch
    {
        uint _searchID;
        string _keyWords;
        List<SearchHit> _results;
        DateTime _requestTime;
        ushort _listIndex;

        public uint ID { get { return _searchID; } set { _searchID = value; } }
        public string KeyWords { get { return _keyWords; } set { _keyWords = value; } }
        public List<SearchHit> Results { get { return _results; } set { _results = value; } }
        public DateTime RequestTime { get { return _requestTime; } set { _requestTime = value; } }
        public uint Age { get { return (uint)(DateTime.Now - _requestTime).TotalSeconds; } }
        public ushort Index { get { return _listIndex; } set { _listIndex = value; } }
    }

    class GuiSearchComparer : IComparer<GuiSearch>
    {
        public enum CompareType
        {
            KeyWords = 0,
            Results = 1,
            RequestTime = 2
        }

        CompareType _whatToCompare;
        
        public GuiSearchComparer(CompareType what = CompareType.KeyWords)
        {
            _whatToCompare = what;
        }

        public int Compare(GuiSearch gs1, GuiSearch gs2)
        {
            switch (_whatToCompare)
            {
                case CompareType.Results:
                    return gs1.Results.Count.CompareTo(gs2.Results.Count);
                case CompareType.RequestTime:
                    return gs1.RequestTime.CompareTo(gs2.RequestTime);
                case CompareType.KeyWords:
                default:
                    return gs1.KeyWords.CompareTo(gs2.KeyWords);
            }
        }
    }

    class SearchProcessor
    {
        const bool DEBUG = false;

        Dictionary<uint, string> _pendingSearchReq;
        Dictionary<uint, GuiSearch> _searches;

        Bridge _b;

        public SearchProcessor(Bridge bridge)
        {
            _b = bridge;

            _pendingSearchReq = new Dictionary<uint, string>();
            _searches = new Dictionary<uint, GuiSearch>();
        }

        public void Reset()
        {
            _pendingSearchReq.Clear();            
            _searches.Clear();
            _b.GUI.lb_searches.Items.Clear();
            _b.GUI.lb_searchResults.Items.Clear();
        }

        public void RegisterSearchIDs(uint ReqID, ResponseSearchIds response)
        {
            if (_pendingSearchReq.ContainsKey(ReqID))
            {
                GuiSearch gs = new GuiSearch();
                gs.KeyWords = _pendingSearchReq[ReqID];
                gs.RequestTime = DateTime.Now;
                gs.Results = new List<SearchHit>();
                gs.ID = response.search_id[0]; // for now we only support one ID

                System.Diagnostics.Debug.WriteLineIf(DEBUG, "Search: Adding ID " + gs.ID);
                _searches.Add(gs.ID, gs);
                _pendingSearchReq.Remove(ReqID);

                UpdateSearches();
            }
        }

        public void ProcessSearchResults(ResponseSearchResults response)
        {
            GuiSearch gs = new GuiSearch();
            bool updated = false;
            System.Diagnostics.Debug.WriteLineIf(DEBUG, "Search: Processing " + response.searches.Count + " search results");
            foreach (SearchSet ss in response.searches)
            {
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "Search: Processing ID" + ss.search_id + " with " + ss.hits.Count + " hits");
                if (_searches.ContainsKey(ss.search_id))
                {
                    gs = _searches[ss.search_id];
                    System.Diagnostics.Debug.WriteLineIf(DEBUG, "Search: Updating results (ID " + gs.ID + ")");
                    if (ss.hits.Count > 0)
                        gs.Results.Clear();
                    foreach (SearchHit sh in ss.hits)
                    {
                        updated = false;
                        // update 
                        for (int i = 0; i < gs.Results.Count; i++)
                        {
                            if (gs.Results[i].file.hash == sh.file.hash)
                            {
                                gs.Results[i].no_hits += sh.no_hits;
                                gs.Results[i].file.name = (gs.Results[i].file.name.Length > sh.file.name.Length) ? gs.Results[i].file.name : sh.file.name;
                                updated = true;
                            }
                        }
                        // adding new
                        if (!updated)
                            gs.Results.Add(sh);
                    }
                    _searches[ss.search_id] = gs;
                }
            }
            UpdateSearches();
            UpdateSearchResults(_b.GUI.lb_searches.SelectedIndex);
        }

        public void Search(string keyWords)
        {
            keyWords = keyWords.Trim();
            if (keyWords == "")
                return;

            string newKeywords = "";
            string[] strings = keyWords.Split(' ');
            List<string> list = new List<string>();
            list.AddRange(strings);

            ushort length = (ushort)list.Count;
            for (ushort i = 0; i < length; i++)
            {
                if (list[i] != "")
                    newKeywords += list[i] + " ";
                else
                {
                    list.RemoveAt(i);
                    i--;
                    length--;
                }
            }
            
            uint reqID;
            reqID = _b.RPC.SearchBasic(list);

            _pendingSearchReq.Add(reqID, newKeywords.Trim());
        }

        public void GetSearchResults()
        {
            List<uint> searchIDs = new List<uint>();
            GuiSearch[] values = new GuiSearch[_searches.Values.Count];
            _searches.Values.CopyTo(values, 0);
            foreach (GuiSearch gs in values)
            {
                if (gs.Age > 10 && gs.Age < 60)
                {
                    System.Diagnostics.Debug.WriteLineIf(DEBUG, "Search: requesting results for " + gs.ID);
                    searchIDs.Add(gs.ID);
                }
            }
            if (searchIDs.Count > 0)
                //_bridge.RPC.SearchResult(searchIDs); sending IDs doens't work at the moment
                _b.RPC.SearchResult(new List<uint>() { });
        }

        public void UpdateSearches()
        {
            GuiSearch gs2;
            uint selectedID = 0;
            ushort selectedIndex = ushort.MaxValue;

            if (_b.GUI.lb_searches.SelectedIndex != -1)
                if (GetSearchByIndex((ushort)_b.GUI.lb_searches.SelectedIndex, out gs2))
                    selectedID = gs2.ID;


            GuiSearch[] values = new GuiSearch[_searches.Values.Count];
            _searches.Values.CopyTo(values, 0);
            Array.Sort(values, new GuiSearchComparer(GuiSearchComparer.CompareType.RequestTime));
            _b.GUI.lb_searches.Items.Clear();
            foreach (GuiSearch gs in values)
            {
                gs2 = _searches[gs.ID];
                gs2.Index = (ushort)_b.GUI.lb_searches.Items.Count;
                _searches[gs.ID] = gs2;
                _b.GUI.lb_searches.Items.Add(((gs.Results != null) ? gs.Results.Count : 0) + " - " + gs.KeyWords);

                if (gs.ID == selectedID)
                    selectedIndex = gs.Index;
            }

            if (selectedIndex < _b.GUI.lb_searchResults.Items.Count)
                _b.GUI.lb_searches.SelectedIndex = selectedIndex;
        }

        public void UpdateSearchResults(int index)
        {
            if (index == -1)
            {
                _b.GUI.lb_searchResults.Items.Clear();
                return;
            }

            GuiSearch gs = new GuiSearch();
            if (!GetSearchByIndex((ushort)index, out gs))
                return;
            if (gs.Results == null)
                return;

            _b.GUI.lb_searchResults.Items.Clear();
            foreach (SearchHit sh in gs.Results)
            {
                _b.GUI.lb_searchResults.Items.Add(sh.no_hits + " hits - " + Processor.BuildSizeString(sh.file.size) + " - " + sh.file.name);
            }

            //if (index < _b.GUI.lb_searchResults.Items.Count)
            //    _b.GUI.lb_searchResults.SelectedIndex = index;
        }

        public bool GetSearchByIndex(ushort index, out GuiSearch gs)
        {
            gs = new GuiSearch();
            GuiSearch[] values = new GuiSearch[_searches.Values.Count];
            _searches.Values.CopyTo(values, 0);
            foreach (GuiSearch gs2 in values)
            {
                if (gs2.Index == index)
                {
                    gs = gs2;
                    return true;
                }
            }
            return false;
        }

        public void RemoveSearch(int index)
        {
            GuiSearch gs = new GuiSearch();
            if (GetSearchByIndex((ushort)index, out gs))
            {
                _b.RPC.SearchClose(gs.ID);
                _searches.Remove(gs.ID);
                UpdateSearches();
                UpdateSearchResults(-1);
            }
        }

        public void CloseAllSearches()
        {
            GuiSearch[] values = new GuiSearch[_searches.Count];
            _searches.Values.CopyTo(values, 0);
            foreach (GuiSearch gs in values)
            {
                _b.RPC.SearchClose(gs.ID);
            }
        }
    }
}
