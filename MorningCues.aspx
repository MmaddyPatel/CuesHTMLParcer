<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="MorningCues.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1>MORNING CUES (For )</h1>
        <div class="row">
            <div class="col-sm-12">
                <table>
                    <asp:Label runat="server" ID="lbl_FundFlows_FNOTotalActivity">
                    </asp:Label>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <table>
                    <asp:Label runat="server" ID="lbl_FundFlows_Futures">
                    </asp:Label>
                </table>
            </div>

            <div class="col-sm-6">
                <table>
                    <asp:Label runat="server" ID="lbl_FundFlows_Options">
                    </asp:Label>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <table>
                    <asp:Label ID="lbl_Fundflows_Remaining" runat="server"></asp:Label>
                </table>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-6">
                <table>
                    <asp:Label ID="lblOutput" runat="server"></asp:Label>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-4">
                <table class="table">
                    <asp:Label ID="lbl_DollarRupeeIndiaVix" runat="server"></asp:Label>
                </table>
            </div>
        </div>


        <div class="row">
            <div class="col-sm-6">
                <div class="demo-content">
                    <table>
                        <asp:Label ID="lbl_cues_OIChange_Now" runat="server"></asp:Label>
                    </table>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="demo-content">
                    <table>
                        <asp:Label ID="lbl_cues_OIChange_Next" runat="server"></asp:Label>
                    </table>
                </div>
            </div>

        </div>
 <div class="row">
            <div class="col-sm-6">
                <div class="demo-content">
                    <table>
                        <asp:Label ID="lbl_PCR" runat="server"></asp:Label>
                    </table>
                </div>
            </div>
     </div>

        <div class="row">
            <div class="col-sm-6">
                <div class="demo-content">
                    <table>
                        <asp:Label ID="lbl_Max_Nifty_WeeklyMonthly" runat="server"></asp:Label>
                    </table>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="demo-content">
                    <table>
                        <asp:Label ID="lbl_Max_BkxNifty_WeeklyMonthly" runat="server"></asp:Label>
                    </table>
                </div>
            </div>

        </div>


        <div class="row">
            <div class="col-sm-6">
                <div class="demo-content">
                    <table>
                        <asp:Label runat="server" ID="lbl_NiftyWeeklyPut">
                        </asp:Label>
                    </table>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="demo-content">
                    <table>
                        <asp:Label runat="server" ID="lbl_NiftyWeeklyCall">
                        </asp:Label>
                    </table>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="demo-content">
                    <table>
                        <asp:Label runat="server" ID="lbl_NiftyMonthlyPut">
                        </asp:Label>
                    </table>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="demo-content">
                    <table>
                        <asp:Label runat="server" ID="lbl_NiftyMonthlyCall">
                        </asp:Label>
                    </table>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-sm-6">
                <div class="demo-content">
                    <table>
                        <asp:Label runat="server" ID="lbl_BkxNiftyWeeklyPut">
                        </asp:Label>
                    </table>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="demo-content">
                    <table>
                        <asp:Label runat="server" ID="lbl_BkxNiftyWeeklyCall">
                        </asp:Label>
                    </table>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="demo-content">
                    <table>
                        <asp:Label runat="server" ID="lbl_BkxNiftyMonthlyPut">
                        </asp:Label>
                    </table>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="demo-content">
                    <table>
                        <asp:Label runat="server" ID="lbl_BkxNiftyMonthlyCall">
                        </asp:Label>
                    </table>
                </div>
            </div>
        </div>
    </div>






</asp:Content>
