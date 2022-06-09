using Commons;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
public class OLanguages
{
    public OLanguages()
    {
    }

    public void AddLanguage(ILanguages objLanguage)
    {
        SqlHelper.ExecuteNonQuery(IConnections.CNStr, "AddLaguage", objLanguage.MS_MODULE, objLanguage.FORM, objLanguage.KEYWORD, objLanguage.VIETNAM, objLanguage.ENGLISH);
    }

    public void UpdateLanguage(ILanguages objLanguage)
    {
        SqlHelper.ExecuteNonQuery(IConnections.CNStr, "UpdateLaguage", objLanguage.STT, objLanguage.MS_MODULE, objLanguage.FORM, objLanguage.KEYWORD, objLanguage.VIETNAM, objLanguage.ENGLISH);
    }

    public void DeleteLanguage(int ID)
    {
        SqlHelper.ExecuteNonQuery(IConnections.CNStr, "DeleteLaguage");
    }

    public string GetLanguage(string FormName, string Keyword)
    {
        string sStr;
        try
        {
            sStr = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spGetNN", Modules.ModuleName, FormName, Keyword, Modules.TypeLanguage)).Replace(@"\\", @"\");
        }
        catch
        {
            sStr = "?" + Keyword + "?";
        }
        return sStr;
    }


    public string GetLanguage(string ModuleName, string FormName, string Keyword, int TypeLanguage)
    {
        string sStr;
        try
        {
            sStr = (SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spGetNN", ModuleName, FormName, Keyword, TypeLanguage)).ToString().Replace(@"\\", @"\");
        }
        catch 
        {
            sStr = "?" + Keyword + "?";
        }
        return sStr;
    }
    public DataTable GetLanguages()
    {
        DataTable dtTable = new DataTable();
        dtTable.Load(SqlHelper.ExecuteReader(IConnections.CNStr, "GetLanguages"));
        return dtTable;
    }
}
