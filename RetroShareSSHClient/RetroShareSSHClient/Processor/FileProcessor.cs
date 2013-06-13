using System;
using System.Collections.Generic;
using System.Windows.Forms;

using rsctrl.core;
using rsctrl.files;

namespace RetroShareSSHClient
{
    struct GuiFileTransfer
    {
        FileTransfer _fileTransfer;
        ushort _listIndex;
        //bool @new;

        public FileTransfer FileTransfer { get { return _fileTransfer; } set { _fileTransfer = value; } }
        //public File File { get { return _fileTransfer.file; } set { _fileTransfer.file = value; } }
        //public Direction Direction { get { return _fileTransfer.direction; } set { _fileTransfer.direction = value; } }
        //public float Fraction { get { return _fileTransfer.fraction; } set { _fileTransfer.fraction = value; } }
        //public float Speed { get { return _fileTransfer.rate_kBs; } set { _fileTransfer.rate_kBs = value; } }
        public string Hash { get { return _fileTransfer.file.hash; } set { _fileTransfer.file.hash = value; } }
        public ushort Index { get { return _listIndex; } set { _listIndex = value; } }
        //public bool @New { get { return @new; } set { @new = value; } }

        public bool Download { get { return (_fileTransfer.direction == Direction.DIRECTION_DOWNLOAD); } }
        public bool Upload { get { return (_fileTransfer.direction == Direction.DIRECTION_UPLOAD); } }
    }

    class GuiFileTransferComparer : IComparer<GuiFileTransfer>
    {
        public int Compare(GuiFileTransfer p1, GuiFileTransfer p2)
        {
            return p1.FileTransfer.file.name.CompareTo(p2.FileTransfer.file.name);
        }
    }

    class FileProcessor
    {
        const bool DEBUG_FILES = false;   

        Dictionary<string, GuiFileTransfer> _fileTransfers;
        Dictionary<string, FileTransfer> _fileTransfersNEW;
        Dictionary<uint, Direction> _pendingRequests;

        Bridge _b;

        public FileProcessor()
        {
            _b = Bridge.GetBridge();

            _fileTransfers = new Dictionary<string, GuiFileTransfer>();
            _fileTransfersNEW = new Dictionary<string, FileTransfer>();
            _pendingRequests = new Dictionary<uint, Direction>();

            //_b.GUI.dgv_filesDownloads.Columns.Add("name", "file name");
            //_b.GUI.dgv_filesDownloads.Columns.Add("speed", "speed [KiB/s]");
            //_b.GUI.dgv_filesDownloads.Columns.Add("done", "% done");
            //_b.GUI.dgv_filesDownloads.Columns.Add("size", "file size");
            _b.GUI.dgv_filesDownloads.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _b.GUI.dgv_filesDownloads.MultiSelect = false;

            //_b.GUI.dgv_filesUploads.Columns.Add("name", "file name");
            //_b.GUI.dgv_filesUploads.Columns.Add("speed", "speed [KiB/s]");
            //_b.GUI.dgv_filesUploads.Columns.Add("size", "file size");
            _b.GUI.dgv_filesUploads.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _b.GUI.dgv_filesUploads.MultiSelect = false;
        }

        internal void Reset()
        {
            _fileTransfers.Clear();
            _fileTransfersNEW.Clear();
            _pendingRequests.Clear();
            _b.GUI.lb_filesDownloads.Items.Clear();
            _b.GUI.lb_filesUploads.Items.Clear();

            _b.GUI.dgv_filesDownloads.Rows.Clear();
            _b.GUI.dgv_filesUploads.Rows.Clear();
        }

        internal void UpdateFileTransfers(ResponseTransferList list, uint reqID)
        {
            List<string> hashes = new List<string>();

            // get direction(DL/UL) of request/result
            Direction dir;
            if (_pendingRequests.TryGetValue(reqID, out dir)) { }
            //_pendingRequests.Remove(reqID);
            else
                if (list.transfers.Count > 0)
                    dir = list.transfers[0].direction;
                else
                    // shouldn't end here 
                    return;

            // add new / update old
            foreach (FileTransfer ft in list.transfers)
            {
                if (ft == null)
                    continue;
                if (_fileTransfers.ContainsKey(ft.file.hash)) // update
                {
                    GuiFileTransfer gft = _fileTransfers[ft.file.hash];
                    gft.FileTransfer = ft;
                    _fileTransfers[ft.file.hash] = gft;
                    System.Diagnostics.Debug.WriteLineIf(DEBUG_FILES, "FileTransfer: updating " + ft.file.hash);
                }
                else //new
                {
                    GuiFileTransfer gft = new GuiFileTransfer();
                    gft.FileTransfer = ft;
                    _fileTransfers.Add(gft.Hash, gft);
                    System.Diagnostics.Debug.WriteLineIf(DEBUG_FILES, "FileTransfer: adding " + ft.file.hash);
                }
                hashes.Add(ft.file.hash);
            }


            // remove
            GuiFileTransfer[] fileTransfers = new GuiFileTransfer[_fileTransfers.Values.Count];
            _fileTransfers.Values.CopyTo(fileTransfers, 0);
            foreach (GuiFileTransfer gft in fileTransfers)
            {
                if (!hashes.Contains(gft.Hash) && gft.FileTransfer.direction == dir)
                {
                    _fileTransfers.Remove(gft.Hash);
                }
            }

            UpdateFileLists(dir);
            //UpdateFileListsNEW(dir);
        }

        internal void UpdateFileTransfersNEW(ResponseTransferList list, uint reqID)
        {
            List<string> hashes = new List<string>();

            // get direction(DL/UL) of request/result
            Direction dir;
            if (_pendingRequests.TryGetValue(reqID, out dir))
                _pendingRequests.Remove(reqID);
            else
                if (list.transfers.Count > 0)
                    dir = list.transfers[0].direction;
                else
                    // shouldn't end here 
                    return;

            _fileTransfersNEW.Clear();

            // map new file transfers to their file hashes
            //Dictionary<string, FileTransfer> mapHashToFileTransfer = new Dictionary<string,FileTransfer>();
            foreach (FileTransfer fileTransfer in list.transfers)
                _fileTransfersNEW.Add(fileTransfer.file.hash, fileTransfer);

            //// add new - update old
            //foreach (KeyValuePair<string, FileTransfer> pair in mapHashToFileTransfer)
            //    if (_fileTransfersNEW.ContainsKey(pair.Key))
            //        _fileTransfersNEW[pair.Key] = pair.Value;
            //    else
            //        _fileTransfersNEW.Add(pair.Key, pair.Value);
            //// remove
            //FileTransfer[] fileTransfers = new FileTransfer[_fileTransfersNEW.Values.Count];
            //_fileTransfersNEW.Values.CopyTo(fileTransfers, 0);
            //foreach (FileTransfer ft in fileTransfers)
            //    if(!mapHashToFileTransfer.ContainsKey(ft.file.hash))
            //        _fileTransfersNEW.Remove(ft.file.hash);

            FileTransfer ft;
            for (int i = 0; i < 50; i++)
            {
                ft = new FileTransfer();
                ft.file = new File();

                ft.direction = Direction.DIRECTION_DOWNLOAD;
                ft.file.name = i.ToString();
                ft.rate_kBs = i;
                ft.fraction = i / 100;
                ft.file.size = (ulong)i;
                ft.file.hash = i.ToString();
                _fileTransfersNEW.Add(ft.file.hash, ft);
            }
           
            UpdateFileListsNEW(dir);
        }

        private void UpdateFileLists(Direction dir)
        {
            // get all transfer for one direction (-> list)
            List<GuiFileTransfer> list = new List<GuiFileTransfer>();
            Dictionary<string, GuiFileTransfer>.ValueCollection fileTransfers = _fileTransfers.Values;
            foreach (GuiFileTransfer gft in fileTransfers)
                if (dir == gft.FileTransfer.direction)
                    list.Add(gft);

            list.Sort(new GuiFileTransferComparer());

            // save selected transfer (-> hash )
            string hash = "";
            GuiFileTransfer tmpGft;
            if (dir == Direction.DIRECTION_DOWNLOAD)
            {
                if (_b.GUI.lb_filesDownloads.SelectedIndex != -1)
                    if (GetFileTransferByListIndexDir(_b.GUI.lb_filesDownloads.SelectedIndex, dir, out tmpGft))
                        hash = tmpGft.Hash;
            }
            else
            {
                if (_b.GUI.lb_filesUploads.SelectedIndex != -1)
                    if (GetFileTransferByListIndexDir(_b.GUI.lb_filesUploads.SelectedIndex, dir, out tmpGft))
                        hash = tmpGft.Hash;
            }

            // build string for each transfer (save to list2)
            string s;
            ushort index = 0;
            int selectedItem = -1;
            GuiFileTransfer gft2;
            string[] list2 = new string[list.Count];

            foreach (GuiFileTransfer gft in list)
            {
                s = BuildFileTransferString(gft.FileTransfer);
                list2[index] = s;

                gft2 = _fileTransfers[gft.Hash];
                gft2.Index = index;
                _fileTransfers[gft.Hash] = gft2;

                if (hash == gft.Hash)
                    selectedItem = index;

                index++;
            }

            // update GUI
            if (dir == Direction.DIRECTION_DOWNLOAD)
            {
                _b.GUI.lb_filesDownloads.Items.Clear();
                _b.GUI.lb_filesDownloads.Items.AddRange(list2);
                _b.GUI.lb_filesDownloads.SelectedIndex = selectedItem;
            }
            else
            {
                _b.GUI.lb_filesUploads.Items.Clear();
                _b.GUI.lb_filesUploads.Items.AddRange(list2);
                _b.GUI.lb_filesUploads.SelectedIndex = selectedItem;
            }

        }

        private void UpdateFileListsNEW(Direction dir)
        {
            // get all transfer for one direction and map them to their hash (-> list)
            Dictionary<string, FileTransfer> list = new Dictionary<string, FileTransfer>();
            Dictionary<string, FileTransfer>.ValueCollection fileTransfers = _fileTransfersNEW.Values;
            foreach (FileTransfer ft in fileTransfers)
                if (dir == ft.direction)
                    list.Add(ft.file.hash, ft);

            if (list.Count == 0)
            {
                if (dir == Direction.DIRECTION_DOWNLOAD)
                    _b.GUI.dgv_filesDownloads.Rows.Clear();
                else
                    _b.GUI.dgv_filesUploads.Rows.Clear();
                return;
            }
            

            object[] row;
            byte hashIndex;
            string selectedHash;
            // get all rows of the DataGridView and map them to their hash (-> list)
            Dictionary<string, object[]> rows = new Dictionary<string, object[]>();
            DataGridViewRowCollection dgvRowCollection;
            if (dir == Direction.DIRECTION_DOWNLOAD)
            {
                hashIndex = 5;
                dgvRowCollection = _b.GUI.dgv_filesDownloads.Rows;
                row = new object[6];

                foreach (DataGridViewRow dgvRow in dgvRowCollection)
                {
                    row[0] = (bool)dgvRow.Cells[0].Value;
                    row[1] = (string)dgvRow.Cells[1].Value;
                    row[2] = (string)dgvRow.Cells[2].Value;
                    row[3] = (string)dgvRow.Cells[3].Value;
                    row[4] = (string)dgvRow.Cells[4].Value;
                    row[5] = (string)dgvRow.Cells[5].Value;

                    rows.Add(row[hashIndex].ToString(), row);

                    if (dgvRow.Selected)
                        selectedHash = (string)row[5];
                }
            }
            else
            {
                hashIndex = 3;
                dgvRowCollection = _b.GUI.dgv_filesUploads.Rows;
                row = new object[4];

                foreach (DataGridViewRow dgvRow in dgvRowCollection)
                {
                    row[0] = (string)dgvRow.Cells[0].Value;
                    row[1] = (string)dgvRow.Cells[1].Value;
                    row[2] = (string)dgvRow.Cells[2].Value;
                    row[3] = (string)dgvRow.Cells[3].Value;

                    rows.Add(row[hashIndex].ToString(), row);

                    if (dgvRow.Selected)
                        selectedHash = (string)row[3];
                }
            }


            string hash;
            // update old & add new
            foreach (FileTransfer ft in list.Values)
            {
                hash = ft.file.hash;
                if (dir == Direction.DIRECTION_DOWNLOAD)
                {
                    row = new object[] {
                            false,
                            String.Format("{0:0,0.00}", ft.fraction * 100) + "%",
                            String.Format("{0:0,0.00}", ft.rate_kBs),                        
                            ft.file.name,
                            Processor.BuildSizeString(ft.file.size),
                            hash
                        };
                }
                else
                {
                    row = new object[] {
                            String.Format("{0:0,0.00}", ft.rate_kBs),
                            ft.file.name,
                            Processor.BuildSizeString(ft.file.size),
                            hash
                        };
                }
                if (rows.ContainsKey(hash)) // update                    
                    rows[hash] = row;
                else //add new
                    rows.Add(hash, row);
            }

            // remove old
            object[][] values = new object[rows.Count][];
            rows.Values.CopyTo(values, 0);
            foreach (object[] row2 in values)
                if (!list.ContainsKey(row2[hashIndex].ToString()))
                    rows.Remove(row[hashIndex].ToString());
           

            if (dir == Direction.DIRECTION_DOWNLOAD)
            {
                _b.GUI.dgv_filesDownloads.Rows.Clear();
                foreach (object[] row2 in rows.Values)
                //{
                    _b.GUI.dgv_filesDownloads.Rows.Add(row2);
                //    if((string)row2[5] == selectedHash)
                //        _b.GUI.dgv_filesDownloads.Rows[_b.GUI.dgv_filesDownloads.se
                //}
            }
            else
            {
                _b.GUI.dgv_filesUploads.Rows.Clear();
                foreach (object[] row2 in rows.Values)
                    _b.GUI.dgv_filesUploads.Rows.Add(row2);
            }
        }

        private string BuildFileTransferString(FileTransfer ft)
        {
            return /* ft.state.ToString() + " - " + */ String.Format("{0:0,0.00}", ft.rate_kBs) + "KiB/s - " +
                ((ft.direction == Direction.DIRECTION_DOWNLOAD) ? (String.Format("{0:0,0.00}", ft.fraction * 100) + "% - ") : "") +
                Processor.BuildSizeString(ft.file.size) + " - " +
                ft.file.name;
        }

        internal bool GetFileTransferBySelection(out GuiFileTransfer gft)
        {
            gft = new GuiFileTransfer();
            if (_b.GUI.lb_filesDownloads.SelectedIndex != -1)
                if (GetFileTransferByListIndexDir(_b.GUI.lb_filesDownloads.SelectedIndex, Direction.DIRECTION_DOWNLOAD, out gft)) { }
                else return false;
            else if (_b.GUI.lb_filesUploads.SelectedIndex != -1)
                if (GetFileTransferByListIndexDir(_b.GUI.lb_filesUploads.SelectedIndex, Direction.DIRECTION_UPLOAD, out gft)) { }
                else return false;
            else return false;
            return true;
        }

        private bool GetFileTransferByListIndexDir(int index, Direction dir, out GuiFileTransfer gft)
        {
            GuiFileTransfer[] values = new GuiFileTransfer[_fileTransfers.Values.Count];
            _fileTransfers.Values.CopyTo(values, 0);
            foreach (GuiFileTransfer ft in values)
            {
                if (ft.Index == index && ft.FileTransfer.direction == dir)
                {
                    gft = ft;
                    return true;
                }
            }
            gft = default(GuiFileTransfer);
            return false;
        }

        internal void RequestFileLists()
        {
            uint i;
            i = _b.RPC.FilesGetTransferList(Direction.DIRECTION_DOWNLOAD);
            _pendingRequests.Add(i, Direction.DIRECTION_DOWNLOAD);

            i = _b.RPC.FilesGetTransferList(Direction.DIRECTION_UPLOAD);
            _pendingRequests.Add(i, Direction.DIRECTION_UPLOAD);
        }

    }
}
