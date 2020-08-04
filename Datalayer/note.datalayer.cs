using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Leaf.Datalayers.Note
{
    public class DataLayer : Leaf.Data.DataLayerBaseMySQL
    {

        public DataLayer(IConfiguration _config)
            : base(new Data.ConnectionDB(_config).getConnectString())
        {

        }

        internal DataTable loadById(int id)
        {
            try
            {
                string sql = @"SELECT * FROM notes WHERE ID = @ID";

                return GetDataTable(sql, new Parameters("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal int Create(Models.Note note)
        {

            string sql = @"INSERT INTO `notes` 
                                (`content`, `creationdate`, 
                                `patientid`, `hash`, `ownerid`) 
                               VALUES
	                           (@CONTENT, NOW(), @PATIENTID, @HASH, @OWNERID)";

            return Execute(sql, new Parameters("CONTENT", DbType.String, note.Content)
                                   , new Parameters("PATIENTID", DbType.Int32, note.Patient.Nhc)
                                   , new Parameters("HASH", DbType.String, Leaf.Tools.MD5Tools.GetMd5Hash(note.Content + note.Patient.Nhc + note.Owner))
                                   , new Parameters("OWNERID", DbType.String, note.Owner.Id)
                                   );

        }

        internal int Save(Models.Note note)
        {
            string sql = @"UPDATE `reports` 
                               SET 
                                `content` = @CONTENT, 
                                `patientid` = @PATIENTID, 
                                `hash` = @HASH
                               WHERE `id` = @ID";

            return Execute(sql, new Parameters("ID", DbType.Int32, note.Id)
                                   , new Parameters("CONTENT", DbType.String, note.Content)
                                   , new Parameters("PATIENTID", DbType.Int32, note.Patient.Nhc)
                                   , new Parameters("HASH", DbType.String, Leaf.Tools.MD5Tools.GetMd5Hash(note.Content + note.Patient.Nhc + note.Owner))
                                   );
        }

        internal List<Models.Note> GetNotesByNHC(int nhc, IConfiguration _config)
        {
            try
            {
                string sql = @"SELECT * FROM notes WHERE PATIENTID = @NHC ORDER BY CREATIONDATE DESC";

                DataTable dt = GetDataTable(sql, new Parameters("NHC", DbType.Int32, nhc));
                List<Models.Note> notes = new List<Models.Note>();

                foreach (DataRow row in dt.Rows)
                {

                    Dictionary<string, string> dic = new Dictionary<string, string> {
                        { "id",             row["id"].ToString() },
                        { "content",        row["content"].ToString() },
                        { "creationDate",   row["creationDate"].ToString() },
                        { "hash",           row["hash"].ToString() },
                        { "patientid",      row["patientid"].ToString() },
                        { "ownerid",        row["ownerid"].ToString() }
                    };

                    Models.Note tmpNote = new Models.Note(
                        new Dictionary<string, string> {
                        { "id",             row["id"].ToString() },
                        { "content",        row["content"].ToString() },
                        { "creationDate",   row["creationDate"].ToString() },
                        { "hash",           row["hash"].ToString() },
                        { "patientid",      row["patientid"].ToString() },
                        { "ownerid",        row["ownerid"].ToString() },
                    }, _config); ;

                    notes.Add(tmpNote);
                }

                return notes;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }


    }
}