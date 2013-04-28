using System;
using System.Collections.Generic;
using System.Xml;

using rsctrl.core;
using rsctrl.files;

namespace RetroShareSSHClient
{
    /*
     * <!DOCTYPE RsCollection>
     * <RsCollection>
     *  <Directory name="TEST dir">
     *   <File size="54806055" sha1="XXX" name="TEST.txt"/>
     *   <File size="54376442" sha1="XXX" name="TEST2.txt"/>
     *  </Directory>
     * </RsCollection>
     */
    static class RsCollection
    {
        const bool DEBUG = false;

        static public bool ReadCollection(string path, out List<File> fileList)
        {
            XmlTextReader tr = new XmlTextReader(path);
            bool foundCollectionTag = false;
            string currentPath = "";

            File tmpFile = new File();
            fileList = new List<File>();

            while (tr.Read())
            {
                if (tr.Name == "") continue;
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "ReadCollection: node name " + tr.Name);
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "ReadCollection: att count " + tr.AttributeCount);

                switch (tr.Name)
                {
                    case "RsCollection":
                        foundCollectionTag = true;
                        break;
                    case "Directory":
                        // save path 
                        try
                        {
                            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ReadCollection: getting file path");
                            currentPath = tr.GetAttribute("name");
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ReadCollection: error reading file path");
                            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ReadCollection: " + e.Message);
                        }
                        break;
                    case "File":
                        // create a tmp file to add it to the list
                        tmpFile = new File();

                        // we need 3 attributes (name, size, hash)
                        if (tr.AttributeCount != 3)
                        {
                            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ReadCollection: file att != 3");
                            break;
                        }

                        // try to add all 3 attributes
                        try
                        {
                            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ReadCollection: adding file");
                            //tmpFile.size = System.Convert.ToUInt32(tr.GetAttribute(0));
                            //tmpFile.hash = tr.GetAttribute(1);
                            //tmpFile.name = tr.GetAttribute(2);
                            tmpFile.hash = tr.GetAttribute("sha1");
                            tmpFile.name = tr.GetAttribute("name");
                            tmpFile.size = System.Convert.ToUInt32(tr.GetAttribute("size"));
                            // TODO add file path
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ReadCollection: error adding file");
                            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ReadCollection: " + e.Message);
                            break;
                        }

                        // the hash is the only thing we can ckeck
                        System.Diagnostics.Debug.WriteLineIf(DEBUG, "ReadCollection: checking file hash");
                        if (tmpFile.hash.Length != 40)
                        {
                            System.Diagnostics.Debug.WriteLineIf(DEBUG, "ReadCollection: error hash.length != 40");
                            break;
                        }

                        // add file to list
                        fileList.Add(tmpFile);                   
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLineIf(DEBUG, "ReadCollection: unknown name " + tr.Name);
                        break;
                } // swicth
            }  // while

            if (!foundCollectionTag)
            {
                // shouldn't happen
                System.Diagnostics.Debug.WriteLineIf(DEBUG, "ReadCollection: no RsCollection tag!");
                fileList = null;
                return false;
            }
            return true;
        }
    }
}
