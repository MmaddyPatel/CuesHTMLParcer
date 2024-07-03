using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CuseHTMLParcer;
using System.Web.Services;

public partial class _Default : Page
{



    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringSqlMkt"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        getdg_FundFlows();
        DollarRupeeIndiaVix();
        Cues_OIChange("M");
        getWeeklyMonthltOI("M");

    }
    private void DollarRupeeIndiaVix()
    {
        string strSql = "exec SP_GET_MORNINGCUES_INR_VIX_2";
        DataSet ds = DbConn.ReturnDataSet(strSql);
        //dg_DollarRupeeIndiaVix.DataSource = ds.Tables[0];
        //dg_DollarRupeeIndiaVix.DataBind();


        string strHTML = "<table class='table table-hover' style='width:100%'>";
        strHTML += "<thead>";
        strHTML += "<tr>";
        strHTML += "<th scope ='col'>Name</th>";
        strHTML += "<th scope ='col'>Price</th>";
        strHTML += "<th scope ='col'>Change</th>";
        strHTML += "<th scope ='col'>%</th>";
        strHTML += "</tr>";
        strHTML += "</thead>";
        strHTML += "<tbody>";

        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {

            strHTML += "<tr>";

            strHTML += "<td>" + ds.Tables[0].Rows[i]["name"] + "</td>";
            strHTML += "<td>" + ds.Tables[0].Rows[i]["last_price"] + "</td>";
            strHTML += "<td>" + ds.Tables[0].Rows[i]["change"].ToString() + "</td>";
            strHTML += "<td>" + ds.Tables[0].Rows[i]["percent_change"] + "%</td>";

            strHTML += "</tr>";
        }
        strHTML += "</tbody>";
        strHTML += "</table>";
        lbl_DollarRupeeIndiaVix.Text = strHTML;
    }
    private void getdg_FundFlows()
    {
        string strHTML = "", strHTMLFundFlow_Options = "", strHTMLFundFlow_Futures = "", strHTMLFundFlow_Remaining = "";

        int numb = 0;
        string HeaderText = "";

        string strSql = "exec SP_GET_MORNINGCUES_FUNDFLOWS_1";
        DataSet ds = DbConn.ReturnDataSet(strSql);

        //for (int tbl = 0; tbl <= ds.Tables[0].Rows.Count - 1; tbl++)
        //{



        strHTML = "<table class='table table-hover' style='width:100%'>";
        strHTML += "<thead>";
        strHTML += "<tr><th scope ='col'>" + HeaderText + "</th></tr>";
        strHTML += "</thead>";
        strHTML += "<tbody>";

        for (int i = 0; i <= ds.Tables[numb].Rows.Count - 1; i++)
        {
            if ((i >= 20))
            {
                if (ds.Tables[0].Rows[i]["FII"].ToString().Contains("Buy"))
                {
                    strHTMLFundFlow_Remaining += "<tr class='success'>";
                }
                else
                {
                    strHTMLFundFlow_Remaining += "<tr class='danger'>";
                }
                strHTMLFundFlow_Remaining += "<td>" + ds.Tables[numb].Rows[i]["FII"] + "</td>";

                strHTMLFundFlow_Remaining += "</tr>";
            }
            else if ((i == 1 || i == 2))
            {
                if (ds.Tables[0].Rows[i]["FII"].ToString().Contains("Buy"))
                {
                    strHTMLFundFlow_Remaining += "<tr class='success'>";
                }
                else
                {
                    strHTMLFundFlow_Remaining += "<tr class='danger'>";
                }
                strHTMLFundFlow_Remaining += "<td>" + ds.Tables[numb].Rows[i]["FII"] + "</td>";

                strHTMLFundFlow_Remaining += "</tr>";
            }
            else if ((i >= 6 && i <= 19))
            {
                if (ds.Tables[0].Rows[i]["FII"].ToString().Contains("FUTURES"))
                {
                    if (ds.Tables[0].Rows[i]["FII"].ToString().Contains("Buy"))
                    {
                        strHTMLFundFlow_Futures += "<tr class='success'>";
                    }
                    else
                    {
                        strHTMLFundFlow_Futures += "<tr class='danger'>";
                    }
                    strHTMLFundFlow_Futures += "<td>" + ds.Tables[numb].Rows[i]["FII"] + "</td>";

                    strHTMLFundFlow_Futures += "</tr>";
                }

                else
                {
                    if (ds.Tables[0].Rows[i]["FII"].ToString().Contains("Buy"))
                    {
                        strHTMLFundFlow_Options += "<tr class='success'>";
                    }
                    else
                    {
                        strHTMLFundFlow_Options += "<tr class='danger'>";
                    }
                    strHTMLFundFlow_Options += "<td>" + ds.Tables[numb].Rows[i]["FII"] + "</td>";

                    strHTMLFundFlow_Options += "</tr>";
                }
            }
        }
        lbl_FundFlows_FNOTotalActivity.Text = "<h1>" + ds.Tables[0].Rows[4]["FII"].ToString() + "</h1>"; ;
        lbl_Fundflows_Remaining.Text = strHTML + "<h3></h3>" + strHTMLFundFlow_Remaining;
        lbl_FundFlows_Futures.Text = strHTML + "<h3>FII ACTIVITY IN FUTURE</h3>" + strHTMLFundFlow_Futures;
        lbl_FundFlows_Options.Text = strHTML + "<h3>FII ACTIVITY IN  OPTION</h3>" + strHTMLFundFlow_Options;
        strHTML += "</tbody>";
        strHTML += "</table>";


        lbl_Fundflows_Remaining.Text = lbl_Fundflows_Remaining.Text + strHTML;
        lbl_FundFlows_Options.Text = lbl_FundFlows_Options.Text + strHTML;
    }



    private void getWeeklyMonthltOI(string type)
    {
        string strSql = "exec SP_GET_DAYCUES_WK_MTH_OI_4 1000000,1000000,100000,100000,'" + type + "'";
        DataSet ds = DbConn.ReturnDataSet(strSql);

        string NiftyWeekly = "", NiftyMonthly = "", BankNiftyWeekly = "", BankNiftyMonthly = "";
        string strHTML = "", strHTML_Head = "", strHTML_Foot = "";


        NiftyWeekly = "(" + ds.Tables[8].Rows[0]["nifty weekly"].ToString() + ")";
        NiftyMonthly = "(" + ds.Tables[9].Rows[0]["nifty monthly"].ToString() + ")";
        BankNiftyWeekly = "(" + ds.Tables[10].Rows[0]["bank nifty weekly"].ToString() + ")";
        BankNiftyMonthly = "(" + ds.Tables[11].Rows[0]["bank nifty monthly"].ToString() + ")";



        int numb = 0;
        string HeaderText = "";
        for (int tbl = 0; tbl <= 7; tbl++)
        {
            if (tbl == 0)
            {
                numb = 2;
                HeaderText = "Nifty Weekly Call " + NiftyWeekly;
            }
            else if (tbl == 1)
            {
                numb = 0;
                HeaderText = "Nifty Weekly Put " + NiftyWeekly;
            }
            else if (tbl == 2)
            {
                numb = 3;
                HeaderText = "Nifty Monthly Call " + NiftyMonthly;
            }
            else if (tbl == 3)
            {
                numb = 1;
                HeaderText = "Nifty Monthly Put " + NiftyMonthly;
            }


            else if (tbl == 4)
            {
                numb = 6;
                HeaderText = "Bk Nifty Weekly Call " + BankNiftyWeekly;
            }
            else if (tbl == 5)
            {
                numb = 4;
                HeaderText = "Bk Nifty Weekly Put " + BankNiftyWeekly;
            }
            else if (tbl == 6)
            {
                numb = 7;
                HeaderText = "Bk Nifty Monthly Call " + BankNiftyMonthly;
            }
            else if (tbl == 7)
            {
                numb = 5;
                HeaderText = "Bk Nifty Monthly Put " + BankNiftyMonthly;
            }


            strHTML = "<table class='table table-hover' style='width:100%'>";
            strHTML = strHTML + "<thead>";
            strHTML = strHTML + "<tr><th scope ='col'>" + HeaderText + "</th></tr>";

            strHTML = strHTML + "</thead>";
            strHTML = strHTML + "<tbody>";

            strHTML += "<tr class='success'><td></td><td></td>";
            strHTML += "<td>OI Change</td>";
            strHTML += "<td>OI</td>";
            strHTML += "</tr>";
            for (int i = 0; i <= ds.Tables[numb].Rows.Count - 1; i++)
            {

                if ((tbl == 0 && ds.Tables[numb].Rows[i]["strike_price"].ToString() == nif_w_c) || (tbl == 1 && ds.Tables[numb].Rows[i]["strike_price"].ToString() == nif_w_p) || (tbl == 2 && ds.Tables[numb].Rows[i]["strike_price"].ToString() == nif_m_c) || (tbl == 3 && ds.Tables[numb].Rows[i]["strike_price"].ToString() == nif_m_p) || (tbl == 4 && ds.Tables[numb].Rows[i]["strike_price"].ToString() == Bknif_w_c) || (tbl == 5 && ds.Tables[numb].Rows[i]["strike_price"].ToString() == Bknif_w_p) || (tbl == 6 && ds.Tables[numb].Rows[i]["strike_price"].ToString() == Bknif_m_c) || (tbl == 7 && ds.Tables[numb].Rows[i]["strike_price"].ToString() == Bknif_m_p))
                {
                    strHTML += "<tr class='success'>";
                }
                else
                {
                    strHTML += "<tr>";
                }
                strHTML += "<td>" + ds.Tables[numb].Rows[i]["strike_price"] + "</td>";
                strHTML += "<td>" + ds.Tables[numb].Rows[i]["option_type"] + "</td>";
                strHTML += "<td>" + ds.Tables[numb].Rows[i]["open_interest_change"] + "</td>";
                strHTML += "<td>" + ds.Tables[numb].Rows[i]["open_interest"] + "</td>";

                strHTML += "</tr>";


            }
            strHTML = strHTML + "</tbody>";
            strHTML = strHTML + "</table>";

            if (tbl == 0)
            {
                lbl_NiftyWeeklyCall.Text = strHTML_Head + strHTML + strHTML_Foot;
            }
            else if (tbl == 1)
            {
                lbl_NiftyWeeklyPut.Text = strHTML_Head + strHTML + strHTML_Foot;
            }
            else if (tbl == 2)
            {
                lbl_NiftyMonthlyCall.Text = strHTML_Head + strHTML + strHTML_Foot;
            }
            else if (tbl == 3)
            {
                lbl_NiftyMonthlyPut.Text = strHTML_Head + strHTML + strHTML_Foot;
            }


            else if (tbl == 4)
            {
                lbl_BkxNiftyWeeklyCall.Text = strHTML_Head + strHTML + strHTML_Foot;
            }
            else if (tbl == 5)
            {
                lbl_BkxNiftyWeeklyPut.Text = strHTML_Head + strHTML + strHTML_Foot;
            }
            else if (tbl == 6)
            {
                lbl_BkxNiftyMonthlyCall.Text = strHTML_Head + strHTML + strHTML_Foot;
            }
            else if (tbl == 7)
            {
                lbl_BkxNiftyMonthlyPut.Text = strHTML_Head + strHTML + strHTML_Foot;
            }
        }



    }
    string nif_w_p, nif_w_c, nif_m_p, nif_m_c, Bknif_w_p, Bknif_w_c, Bknif_m_p, Bknif_m_c;
    private void Cues_OIChange(string type)
    {
        string strHTML, strHTML_Now, strHTML_Next, str_Max_Nifty_WeeklyMonthly, str_Max_BkNifty_WeeklyMonthly;
        string[] split;


        string strSql = "SP_GET_M_D_CUES_MAX_4";
        DataSet ds = DbConn.ReturnDataSet(strSql);


        //strHTML = "<html><body><table border='1' width='400px' height='auto' width='80%' >";
        //strHTML = strHTML + "<div class='container'>";
        //strHTML = strHTML + "<div>";
        //strHTML = strHTML + "<div class='col-md-3'>";



        /*NSX JUNE SERIES*/
        strHTML_Now = "<table class='table table-hover' style='width:90%'>";

        strHTML_Now = strHTML_Now + "<thead>";
        strHTML_Now = strHTML_Now + "<tr><th scope ='col'>" + getData(6, 1, ds) + "</th></tr>";

        strHTML_Now = strHTML_Now + "</thead>";


        strHTML_Now = strHTML_Now + "<tbody>";
        //strHTML_Now = strHTML_Now + "<tr><td>" + getData(8, 3, ds) + "</td></tr>";
        //strHTML_Now = strHTML_Now + "<tr><td>" + getData(7, 2, ds) + "</td></tr>";
        //strHTML_Now = strHTML_Now + "<tr><td>" + getData(10, 5, ds) + "</td></tr>";
        //strHTML_Now = strHTML_Now + "<tr><td>" + getData(9, 4, ds) + "</td></tr>";
        strHTML_Now = strHTML_Now + "<tr><td>" + ds.Tables[0].Rows[28]["oi"].ToString() + "</td></tr>";

        strHTML_Now = strHTML_Now + "<tr><td>" + ds.Tables[0].Rows[30]["oi"].ToString() + "</td></tr>";
        strHTML_Now = strHTML_Now + "<tr><td>" + ds.Tables[0].Rows[31]["oi"].ToString() + "</td></tr>";

        strHTML_Now = strHTML_Now + "<tr><td>" + ds.Tables[0].Rows[33]["oi"].ToString() + "</td></tr>";

        strHTML_Now = strHTML_Now + "</tr>";
        strHTML_Now = strHTML_Now + "</tbody>";
        strHTML_Now = strHTML_Now + "</table>";

        // strHTML_Now = strHTML_Now + "</thead>";
        lbl_cues_OIChange_Now.Text = strHTML_Now;// + lbl_cues_OIChange_Now.Text;


        strHTML_Next = "<table class='table table-hover'>";

        strHTML_Next = strHTML_Next + "<thead class='thead-dark'>";
        strHTML_Next = strHTML_Next + "<tr><th scope ='col'>" + getData(11, 6, ds).Replace("Nifty", "") + "</th></tr>";

        strHTML_Next = strHTML_Next + "</thead>";
        /*NSX JULY SERIES*/
        strHTML_Next = strHTML_Next + "<tbody>";
        strHTML_Next = strHTML_Next + "<tr><td>" + getData(13, 8, ds) + "</td></tr>";
        strHTML_Next = strHTML_Next + "<tr><td>" + getData(12, 7, ds) + "</td></tr>";
        strHTML_Next = strHTML_Next + "<tr><td>" + getData(15, 10, ds) + "</td></tr>";
        strHTML_Next = strHTML_Next + "<tr><td>" + getData(14, 9, ds) + "</td></tr>";

        strHTML_Next = strHTML_Next + "</tr>";
        strHTML_Next = strHTML_Next + "</tbody>";
        strHTML_Next = strHTML_Next + "</table>";
        lbl_cues_OIChange_Next.Text = strHTML_Next;// + lbl_cues_OIChange_Next.Text;


        /* NSX Max Weekly */
        str_Max_Nifty_WeeklyMonthly = "<h2> NIFTY</h2>";
        str_Max_Nifty_WeeklyMonthly += "<table class='table table-hover'>";

        str_Max_Nifty_WeeklyMonthly += "<thead class='thead-dark'>";
        str_Max_Nifty_WeeklyMonthly += "<tr><th scope ='col'>" + getData(17, 12, ds).Replace("Max Nifty", "").Replace("Options", "") + "</th></tr>";

        str_Max_Nifty_WeeklyMonthly += "</thead>";

        split = ds.Tables[0].Rows[14]["oi"].ToString().Split('~');
        str_Max_Nifty_WeeklyMonthly +=  "<tbody>";
        str_Max_Nifty_WeeklyMonthly += "<tr class='success'><td></td>";
        str_Max_Nifty_WeeklyMonthly += "<td>OI Change</td>";
        str_Max_Nifty_WeeklyMonthly += "<td>OI</td>";
        str_Max_Nifty_WeeklyMonthly += "</tr>";
        str_Max_Nifty_WeeklyMonthly += "<tr>";
        str_Max_Nifty_WeeklyMonthly += "<td  >" + split[0] + "</td>";
        str_Max_Nifty_WeeklyMonthly += "<td  >" + split[1] + "</td>";
        str_Max_Nifty_WeeklyMonthly += "<td>" + split[2] + "</td>";
        str_Max_Nifty_WeeklyMonthly += "</tr>";
        nif_w_p = split[0].Replace("PUT", "").Replace("CALL", "").Trim();

        split = ds.Tables[0].Rows[15]["oi"].ToString().Split('~');

        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<tr>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<td  >" + split[0] + "</td>";

        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<td>" + split[1] + "</td>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<td  >" + split[2] + "</td>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "</tr>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "</tbody>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "</table>";
        nif_w_c = split[0].Replace("PUT", "").Replace("CALL", "").Trim();

        /* NSX Max Monthly */

        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<p>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<table class='table table-hover'>";

        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<thead class='thead-dark'>";


        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<tr><th scope ='col'>" + getData(21, 16, ds).Replace("Max Nifty", "").Replace("Options", "") + "</th></tr>";


        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "</thead>";

        split = ds.Tables[0].Rows[18]["oi"].ToString().Split('~');
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<tbody >";
        str_Max_Nifty_WeeklyMonthly += "<tr class='success'><td></td>";
        str_Max_Nifty_WeeklyMonthly += "<td>OI Change</td>";
        str_Max_Nifty_WeeklyMonthly += "<td>OI</td>";
        str_Max_Nifty_WeeklyMonthly += "</tr>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<tr>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<td >" + split[0] + "</td>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<td  >" + split[1] + "</td>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<td>" + split[2] + "</td>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "</tr>";
        nif_m_p = split[0].Replace("PUT", "").Replace("CALL", "").Trim();

        split = ds.Tables[0].Rows[19]["oi"].ToString().Split('~');
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<tr>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<td  >" + split[0] + "</td>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<td  >" + split[1] + "</td>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "<td>" + split[2] + "</td>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "</tr>";
        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "</tbody>";

        str_Max_Nifty_WeeklyMonthly = str_Max_Nifty_WeeklyMonthly + "</table>";
        nif_m_c = split[0].Replace("PUT", "").Replace("CALL", "").Trim();



        /* BKX START*/

        str_Max_BkNifty_WeeklyMonthly = "<h2>BANK NIFTY</h2>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<p>";


        /* BKX Max Weekly */

        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<table class='table table-hover'>";

        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<thead class='thead-dark'>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<tr><th>" + ds.Tables[0].Rows[20]["oi"].ToString().Replace("Max Bank Nifty", "").Replace("Options", "") + "</th></tr>";

        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "</thead>";

        split = ds.Tables[0].Rows[22]["oi"].ToString().Split('~');
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<tbody>";
        str_Max_BkNifty_WeeklyMonthly += "<tr class='success'><td></td>";
        str_Max_BkNifty_WeeklyMonthly += "<td>OI Change</td>";
        str_Max_BkNifty_WeeklyMonthly += "<td>OI</td>";
        str_Max_BkNifty_WeeklyMonthly += "</tr>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<tr>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<td  >" + split[0] + "</td>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<td  >" + split[1] + "</td>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<td>" + split[2] + "</td>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "</tr>";
        Bknif_w_p = split[0].Replace("PUT", "").Replace("CALL", "").Trim();
        split = ds.Tables[0].Rows[23]["oi"].ToString().Split('~');

        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<tr>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<td  >" + split[0] + "</td>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<td  >" + split[1] + "</td>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<td>" + split[2] + "</td>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "</tr>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "</tbody>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "</table>";
        Bknif_w_c = split[0].Replace("PUT", "").Replace("CALL", "").Trim();

        /* BKX Max Monthly */
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<table class='table table-hover'>";

        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<thead class='thead-dark'>";


        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<tr ><th scope ='col'>" + ds.Tables[0].Rows[24]["oi"].ToString().Replace("Max Bank Nifty", "").Replace("Options", "") + "</th></tr>";


        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "</thead>";

        split = ds.Tables[0].Rows[26]["oi"].ToString().Split('~');
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<tbody >";
        str_Max_BkNifty_WeeklyMonthly += "<tr class='success'><td></td>";
        str_Max_BkNifty_WeeklyMonthly += "<td>OI Change</td>";
        str_Max_BkNifty_WeeklyMonthly += "<td>OI</td>";
        str_Max_BkNifty_WeeklyMonthly += "</tr>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<tr>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<td >" + split[0] + "</td>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<td  >" + split[1] + "</td>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<td>" + split[2] + "</td>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "</tr>";
        Bknif_m_p = split[0].Replace("PUT", "").Replace("CALL", "").Trim();

        split = ds.Tables[0].Rows[27]["oi"].ToString().Split('~');
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<tr>";
        
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<td  >" + split[0] + "</td>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<td  >" + split[1] + "</td>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "<td>" + split[2] + "</td>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "</tr>";
        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "</tbody>";

        str_Max_BkNifty_WeeklyMonthly = str_Max_BkNifty_WeeklyMonthly + "</table>";
        Bknif_m_c = split[0].Replace("PUT", "").Replace("CALL", "").Trim();
        /*BKX END */
        //strHTML = strHTML + "</div>";
        //strHTML = strHTML + "</div>";

        //strHTML = strHTML + "</div>";

        lbl_Max_Nifty_WeeklyMonthly.Text = str_Max_Nifty_WeeklyMonthly;
        lbl_Max_BkxNifty_WeeklyMonthly.Text = str_Max_BkNifty_WeeklyMonthly;




        /*NSX JUNE SERIES*/
        strHTML_Now = "<table class='table table-hover' style='width:90%'>";

        strHTML_Now = strHTML_Now + "<thead>";
        strHTML_Now = strHTML_Now + "<tr><th scope ='col'>OI and PCR</th></tr>";

        strHTML_Now = strHTML_Now + "</thead>";


        strHTML_Now = strHTML_Now + "<tbody>";


        strHTML_Now = strHTML_Now + "<tr><td>" + ds.Tables[0].Rows[29]["oi"].ToString() + "</td></tr>";
        strHTML_Now = strHTML_Now + "<tr><td>" + ds.Tables[0].Rows[32]["oi"].ToString() + "</td></tr>";
        strHTML_Now = strHTML_Now + "<tr><td>" + ds.Tables[0].Rows[34]["oi"].ToString() + "</td></tr>";
        strHTML_Now = strHTML_Now + "<tr><td>" + ds.Tables[0].Rows[35]["oi"].ToString() + "</td></tr>";


        strHTML_Now = strHTML_Now + "</tr>";
        strHTML_Now = strHTML_Now + "</tbody>";
        strHTML_Now = strHTML_Now + "</table>";


        lbl_PCR.Text = strHTML_Now;


        // lblOutput.Text = strHTML;
    }
    //private void Cues_OIChange_Orig(string type)
    //{
    //    string strHTML;
    //    string[] split;
    //    strHTML = "<html><body><table border='1' width='400px' height='auto' width='80%' >";

    //    string strSql = "SP_GET_DAYCUES_MAX_3";
    //    DataSet ds = DbConn.ReturnDataSet(strSql);



    //    strHTML = "<div class='container'>";
    //    strHTML = "<div>";
    //    strHTML = strHTML + "<div class='col-md-3'>";



    //    /*NSX JUNE SERIES*/
    //    strHTML = strHTML + "<table class='table table-hover' style='width:90%'>";

    //    strHTML = strHTML + "<thead>";
    //    strHTML = strHTML + "<tr><th scope ='col'>" + getData(6, 1, ds) + "</th></tr>";

    //    strHTML = strHTML + "</thead>";


    //    strHTML = strHTML + "<tbody>";
    //    strHTML = strHTML + "<tr><td>" + getData(8, 3, ds) + "</td></tr>";
    //    strHTML = strHTML + "<tr><td>" + getData(7, 2, ds) + "</td></tr>";
    //    strHTML = strHTML + "<tr><td>" + getData(10, 5, ds) + "</td></tr>";
    //    strHTML = strHTML + "<tr><td>" + getData(9, 4, ds) + "</td></tr>";
    //    strHTML = strHTML + "</tr>";
    //    strHTML = strHTML + "</tbody>";
    //    strHTML = strHTML + "</table>";

    //    strHTML = strHTML + "</thead>";



    //    strHTML = strHTML + "<table class='table table-hover'>";

    //    strHTML = strHTML + "<thead class='thead-dark'>";
    //    strHTML = strHTML + "<tr><th scope ='col'>" + getData(11, 6, ds).Replace("Nifty", "") + "</th></tr>";

    //    strHTML = strHTML + "</thead>";
    //    /*NSX JULY SERIES*/
    //    strHTML = strHTML + "<tbody>";
    //    strHTML = strHTML + "<tr><td>" + getData(13, 8, ds) + "</td></tr>";
    //    strHTML = strHTML + "<tr><td>" + getData(12, 7, ds) + "</td></tr>";
    //    strHTML = strHTML + "<tr><td>" + getData(15, 10, ds) + "</td></tr>";
    //    strHTML = strHTML + "<tr><td>" + getData(14, 9, ds) + "</td></tr>";
    //    strHTML = strHTML + "</tr>";
    //    strHTML = strHTML + "</tbody>";
    //    strHTML = strHTML + "</table>";



    //    /* NSX Max Weekly */
    //    strHTML = strHTML + "<h2> NIFTY</h2>";
    //    strHTML = strHTML + "<table class='table table-hover'>";

    //    strHTML = strHTML + "<thead class='thead-dark'>";
    //    strHTML = strHTML + "<tr><th scope ='col'>" + getData(17, 12, ds).Replace("Max Nifty", "").Replace("Options", "") + "</th></tr>";

    //    strHTML = strHTML + "</thead>";

    //    split = ds.Tables[0].Rows[14]["oi"].ToString().Split('~');
    //    strHTML = strHTML + "<tbody>";
    //    strHTML = strHTML + "<tr>";
    //    strHTML = strHTML + "<td  >" + split[0] + "</td>";
    //    strHTML = strHTML + "<td  >" + split[1] + "</td>";
    //    strHTML = strHTML + "<td>" + split[2] + "</td>";
    //    strHTML = strHTML + "</tr>";

    //    split = ds.Tables[0].Rows[15]["oi"].ToString().Split('~');
    //    strHTML = strHTML + "<tr>";
    //    strHTML = strHTML + "<td  >" + split[0] + "</td>";
    //    strHTML = strHTML + "<td  >" + split[1] + "</td>";
    //    strHTML = strHTML + "<td>" + split[2] + "</td>";
    //    strHTML = strHTML + "</tr>";
    //    strHTML = strHTML + "</tbody>";
    //    strHTML = strHTML + "</table>";


    //    /* NSX Max Monthly */

    //    strHTML = strHTML + "<p>";
    //    strHTML = strHTML + "<table class='table table-hover'>";

    //    strHTML = strHTML + "<thead class='thead-dark'>";


    //    strHTML = strHTML + "<tr><th scope ='col'>" + getData(21, 16, ds).Replace("Max Nifty", "").Replace("Options", "") + "</th></tr>";


    //    strHTML = strHTML + "</thead>";

    //    split = ds.Tables[0].Rows[18]["oi"].ToString().Split('~');
    //    strHTML = strHTML + "<tbody >";
    //    strHTML = strHTML + "<tr>";
    //    strHTML = strHTML + "<td >" + split[0] + "</td>";
    //    strHTML = strHTML + "<td  >" + split[1] + "</td>";
    //    strHTML = strHTML + "<td>" + split[2] + "</td>";
    //    strHTML = strHTML + "</tr>";

    //    split = ds.Tables[0].Rows[19]["oi"].ToString().Split('~');
    //    strHTML = strHTML + "<tr>";
    //    strHTML = strHTML + "<td  >" + split[0] + "</td>";
    //    strHTML = strHTML + "<td  >" + split[1] + "</td>";
    //    strHTML = strHTML + "<td>" + split[2] + "</td>";
    //    strHTML = strHTML + "</tr>";
    //    strHTML = strHTML + "</tbody>";

    //    strHTML = strHTML + "</table>";




    //    /* BKX START*/

    //    strHTML = strHTML + "<h2>BANK NIFTY</h2>";
    //    strHTML = strHTML + "<p>";


    //    /* BKX Max Weekly */

    //    strHTML = strHTML + "<table class='table table-hover'>";

    //    strHTML = strHTML + "<thead class='thead-dark'>";
    //    strHTML = strHTML + "<tr><th>" + ds.Tables[0].Rows[20]["oi"].ToString().Replace("Max Bank Nifty", "").Replace("Options", "") + "</th></tr>";

    //    strHTML = strHTML + "</thead>";

    //    split = ds.Tables[0].Rows[22]["oi"].ToString().Split('~');
    //    strHTML = strHTML + "<tbody>";
    //    strHTML = strHTML + "<tr>";
    //    strHTML = strHTML + "<td  >" + split[0] + "</td>";
    //    strHTML = strHTML + "<td  >" + split[1] + "</td>";
    //    strHTML = strHTML + "<td>" + split[2] + "</td>";
    //    strHTML = strHTML + "</tr>";

    //    split = ds.Tables[0].Rows[23]["oi"].ToString().Split('~');
    //    strHTML = strHTML + "<tr>";
    //    strHTML = strHTML + "<td  >" + split[0] + "</td>";
    //    strHTML = strHTML + "<td  >" + split[1] + "</td>";
    //    strHTML = strHTML + "<td>" + split[2] + "</td>";
    //    strHTML = strHTML + "</tr>";
    //    strHTML = strHTML + "</tbody>";
    //    strHTML = strHTML + "</table>";


    //    /* BKX Max Monthly */
    //    strHTML = strHTML + "<table class='table table-hover'>";

    //    strHTML = strHTML + "<thead class='thead-dark'>";


    //    strHTML = strHTML + "<tr ><th scope ='col'>" + ds.Tables[0].Rows[24]["oi"].ToString().Replace("Max Bank Nifty", "").Replace("Options", "") + "</th></tr>";


    //    strHTML = strHTML + "</thead>";

    //    split = ds.Tables[0].Rows[26]["oi"].ToString().Split('~');
    //    strHTML = strHTML + "<tbody >";
    //    strHTML = strHTML + "<tr>";
    //    strHTML = strHTML + "<td >" + split[0] + "</td>";
    //    strHTML = strHTML + "<td  >" + split[1] + "</td>";
    //    strHTML = strHTML + "<td>" + split[2] + "</td>";
    //    strHTML = strHTML + "</tr>";

    //    split = ds.Tables[0].Rows[27]["oi"].ToString().Split('~');
    //    strHTML = strHTML + "<tr>";
    //    strHTML = strHTML + "<td  >" + split[0] + "</td>";
    //    strHTML = strHTML + "<td  >" + split[1] + "</td>";
    //    strHTML = strHTML + "<td>" + split[2] + "</td>";
    //    strHTML = strHTML + "</tr>";
    //    strHTML = strHTML + "</tbody>";

    //    strHTML = strHTML + "</table>";

    //    /*BKX END */
    //    strHTML = strHTML + "</div>";
    //    strHTML = strHTML + "</div>";

    //    strHTML = strHTML + "</div>";




    //    lblOutput.Text = strHTML;
    //}


    private string getData(int sno, int number, DataSet ds)
    {
        //if (Convert.ToInt16(sno) == 20 ||  Convert.ToInt16(sno) == 19 ||  Convert.ToInt16(sno) == 6 || Convert.ToInt16(sno) == 17 || Convert.ToInt16(sno) == 21 || Convert.ToInt16(sno) == 22 || Convert.ToInt16(sno) == 26 || Convert.ToInt16(sno) == 30 || Convert.ToInt16(sno) == 29 || Convert.ToInt16(sno) == 18 || Convert.ToInt16(sno) == 23 || Convert.ToInt16(sno) == 24)

        //    return "<b>" + ds.Tables[0].Rows[sno]["oi"].ToString() + "</b><br>";

        //else
        if (Convert.ToInt16(sno) == 7)

        {
            // DataSet ds = DbConn.ReturnDataSet(strSql);
            DataTable dtRep = DbConn.ReturnSQLData("select name,OPEN_INTEREST, PREVDAY_OPEN_INTEREST,(OPEN_INTEREST-PREVDAY_OPEN_INTEREST) as OPEN_INT_CHANGE,((OPEN_INTEREST - PREVDAY_OPEN_INTEREST) * 100) / (OPEN_INTEREST)AS COND,((OPEN_INTEREST - PREVDAY_OPEN_INTEREST) / PREVDAY_OPEN_INTEREST) * 100 as OPEN_INT_PER_CHANGE from STOCK_ITEMS where EXCHANGE_SYMBOL in ('NIFTY') and EXCHANGE_ID = 3");

            string repString = Math.Round(Convert.ToDouble(dtRep.Rows[0]["OPEN_INT_PER_CHANGE"].ToString()), 2) + "% " + (Convert.ToDecimal(dtRep.Rows[0]["COND"].ToString()) > 0 ? " Adds " : " Sheds ") + Convert.ToDouble(dtRep.Rows[0]["OPEN_INT_CHANGE"].ToString()) + " shares";

            return ds.Tables[0].Rows[number]["oi"].ToString().Replace("@NIF_OICHG", repString) + "<br>";

        }

        else if (Convert.ToInt16(sno) == 8)

        {

            DataTable dtRep = DbConn.ReturnSQLData("exec SP_GET_DAYCUES_FUTSERIES_0");

            string repString = " Fut at " + Math.Round(Convert.ToDouble(dtRep.Rows[0]["POINTS"].ToString()), 2) + "points " + (Convert.ToDecimal(dtRep.Rows[0]["POINTS"].ToString()) > 0 ? " PREM " : " DISC ") + " vs " + Math.Round(Convert.ToDouble(dtRep.Rows[2]["POINTS"].ToString()), 2) + " points " + (Convert.ToDecimal(dtRep.Rows[2]["POINTS"].ToString()) > 0 ? " PREM " : " DISC ");

            return ds.Tables[0].Rows[number]["oi"].ToString().Replace("@curr_nif", repString) + "<br>";

        }

        else if (Convert.ToInt16(sno) == 9)

        {

            DataTable dtRep = DbConn.ReturnSQLData("select name,OPEN_INTEREST, PREVDAY_OPEN_INTEREST,(OPEN_INTEREST-PREVDAY_OPEN_INTEREST) as OPEN_INT_CHANGE,((OPEN_INTEREST - PREVDAY_OPEN_INTEREST) * 100) / (OPEN_INTEREST)AS COND,((OPEN_INTEREST - PREVDAY_OPEN_INTEREST) / PREVDAY_OPEN_INTEREST) * 100 as OPEN_INT_PER_CHANGE from STOCK_ITEMS where EXCHANGE_SYMBOL in ('BANKNIFTY') and EXCHANGE_ID = 3");

            string repString = Math.Round(Convert.ToDouble(dtRep.Rows[0]["OPEN_INT_PER_CHANGE"].ToString()), 2) + "% " + (Convert.ToDecimal(dtRep.Rows[0]["COND"].ToString()) > 0 ? " Adds " : " Sheds ") + Convert.ToDouble(dtRep.Rows[0]["OPEN_INT_CHANGE"].ToString()) + " shares";

            return ds.Tables[0].Rows[number]["oi"].ToString().Replace("@NIFBK_OICHG", repString) + "<br>";

        }

        else if (Convert.ToInt16(sno) == 10)

        {

            DataTable dtRep = DbConn.ReturnSQLData("exec SP_GET_DAYCUES_FUTSERIES_0");

            string repString = " Fut at " + Math.Round(Convert.ToDouble(dtRep.Rows[1]["POINTS"].ToString()), 2) + "points " + (Convert.ToDecimal(dtRep.Rows[1]["POINTS"].ToString()) > 0 ? " PREM " : " DISC ") + " vs " + Math.Round(Convert.ToDouble(dtRep.Rows[3]["POINTS"].ToString()), 2) + " points " + (Convert.ToDecimal(dtRep.Rows[3]["POINTS"].ToString()) > 0 ? " PREM " : " DISC ");

            return ds.Tables[0].Rows[number]["oi"].ToString().Replace("@curr_nifbk", repString) + "<br>";

        }

        else if (Convert.ToInt16(sno) == 11)

            return "<b>" + ds.Tables[0].Rows[number]["oi"].ToString() + "</b><br>";

        else if (Convert.ToInt16(sno) == 12)

        {

            DataTable dtRep = DbConn.ReturnSQLData("select name,OPEN_INTEREST, PREVDAY_OPEN_INTEREST,(OPEN_INTEREST-PREVDAY_OPEN_INTEREST) as OPEN_INT_CHANGE,((OPEN_INTEREST - PREVDAY_OPEN_INTEREST) * 100) / (OPEN_INTEREST)AS COND,((OPEN_INTEREST - PREVDAY_OPEN_INTEREST) / PREVDAY_OPEN_INTEREST) * 100 as OPEN_INT_PER_CHANGE from STOCK_ITEMS where EXCHANGE_SYMBOL in ('NIFTY') and EXCHANGE_ID = 4");

            string repString = Math.Round(Convert.ToDouble(dtRep.Rows[0]["OPEN_INT_PER_CHANGE"].ToString()), 2) + "% " + (Convert.ToDecimal(dtRep.Rows[0]["COND"].ToString()) > 0 ? " Adds " : " Sheds ") + Convert.ToDouble(dtRep.Rows[0]["OPEN_INT_CHANGE"].ToString()) + " shares";

            return ds.Tables[0].Rows[number]["oi"].ToString().Replace("@NIF_OICHGC2", repString) + "<br>";

        }

        else if (Convert.ToInt16(sno) == 13)

        {

            DataTable dtRep = DbConn.ReturnSQLData("exec SP_GET_DAYCUES_FUTSERIES_0");

            string repString = " Fut at " + Math.Round(Convert.ToDouble(dtRep.Rows[4]["POINTS"].ToString()), 2) + "points " + (Convert.ToDecimal(dtRep.Rows[4]["POINTS"].ToString()) > 0 ? " PREM " : " DISC ") + " vs " + Math.Round(Convert.ToDouble(dtRep.Rows[6]["POINTS"].ToString()), 2) + " points " + (Convert.ToDecimal(dtRep.Rows[6]["POINTS"].ToString()) > 0 ? " PREM " : " DISC ");

            return ds.Tables[0].Rows[number]["oi"].ToString().Replace("@curr_nifC2", repString) + "<br>";

        }

        else if (Convert.ToInt16(sno) == 14)

        {

            DataTable dtRep = DbConn.ReturnSQLData("select name,OPEN_INTEREST, PREVDAY_OPEN_INTEREST,(OPEN_INTEREST-PREVDAY_OPEN_INTEREST) as OPEN_INT_CHANGE,((OPEN_INTEREST - PREVDAY_OPEN_INTEREST) * 100) / (OPEN_INTEREST)AS COND,((OPEN_INTEREST - PREVDAY_OPEN_INTEREST) / PREVDAY_OPEN_INTEREST) * 100 as OPEN_INT_PER_CHANGE from STOCK_ITEMS where EXCHANGE_SYMBOL in ('BANKNIFTY') and EXCHANGE_ID = 4");

            string repString = Math.Round(Convert.ToDouble(dtRep.Rows[0]["OPEN_INT_PER_CHANGE"].ToString()), 2) + "% " + (Convert.ToDecimal(dtRep.Rows[0]["COND"].ToString()) > 0 ? " Adds " : " Sheds ") + Convert.ToDouble(dtRep.Rows[0]["OPEN_INT_CHANGE"].ToString()) + " shares";

            return ds.Tables[0].Rows[number]["oi"].ToString().Replace("@NIFBK_OICHGC2", repString) + "<br>";

        }

        else if (Convert.ToInt16(sno) == 15)

        {

            DataTable dtRep = DbConn.ReturnSQLData("exec SP_GET_DAYCUES_FUTSERIES_0");

            string repString = " Fut at " + Math.Round(Convert.ToDouble(dtRep.Rows[5]["POINTS"].ToString()), 2) + "points " + (Convert.ToDecimal(dtRep.Rows[5]["POINTS"].ToString()) > 0 ? " PREM " : " DISC ") + " vs " + Math.Round(Convert.ToDouble(dtRep.Rows[7]["POINTS"].ToString()), 2) + " points " + (Convert.ToDecimal(dtRep.Rows[7]["POINTS"].ToString()) > 0 ? " PREM " : " DISC ");

            return ds.Tables[0].Rows[number]["oi"].ToString().Replace("@curr_nifbkC2", repString) + "<br>";

        }

        else if (Convert.ToInt16(sno) == 16)

            return "<b>" + ds.Tables[0].Rows[number]["oi"].ToString() + "</b><br>";
        else
        {
            return "<b>" + ds.Tables[0].Rows[number]["oi"].ToString() + "</b><br>";
        }
    }
}