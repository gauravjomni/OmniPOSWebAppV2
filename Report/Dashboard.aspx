<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Admin_Dashboard" ValidateRequest="false" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reports</title>
    <link href="../content/table.css" rel="stylesheet" type="text/css" />
    <link href="../content/DatePicker.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.livequery.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        var appPath = (location.protocol + '//' + location.host + '<%=PathName%>').toLowerCase();
    </script>

    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Report.js" type="text/javascript"></script></head>
<body>
    <div id="backgroundFade" class="black_overlay"></div>
    <div id="lightBox2" class="white_content">
        <p class="Process_head">Processing going on ...</p>
        <img src='../Image/progress-bar.gif' alt="Loading" />
    </div>

    <div class="header" style="background-color:#000000;">
        <div class="wrap">
            <div class="logo">
                <img src="../images/logo.png" alt="Omni POS"/>
            </div>
            <ul class="top_nav">
            	<li><a href="#">Go to Home</a></li>
            </ul>
            <%--<div class="search-box"></div>
            <ul class="flexy-menu thick orange">
                <li class="showhide right" style="display: none;"><span class="title">MENU</span><span class="icon"><em></em><em></em><em></em><em></em></span></li>
                <li style="" class="right"><a href="//blog.templatescreme.com/">Blog</a></li>
                <li style="" class="right"><a href="/free-email-templates">E-mail Templates</a></li>
                <li style="" class="right"><a>Categories</a></li>
                <li style="" class="right"><a href="/premium-website-templates">Premium Website</a></li><li style="" class="right"><a href="/free-website-templates">Free Website</a></li><li style="" class="right"><a href="/">Start</a></li>
            </ul>--%>
            <div class="clear"></div>
        </div>
    </div>

    <div class="main-wrapper">
    <div id="header">
        <!-- Header. Main part -->
        <div id="header-main">
            <div class="container_12">
                <div class="grid_12">
                    <div id="logo">
                        <ul id="nav">
                            <li><a href="">Dashboard</a></li>
                            <li id="current"><a href="">Sales</a></li>
                            <li><a href="">Sales History</a></li>
                            <li><a href="">Live Transaction</a></li>
                            <li><a href="">Refund And Payout</a></li>
                        </ul>
                    </div><!-- End. #Logo -->
                </div><!-- End. .grid_12-->
                <div style="clear: both;"></div>
            </div><!-- End. .container_12 -->
        </div> <!-- End #header-main -->
        <div style="clear: both;"></div>
        <!-- Sub navigation -->
            <!-- End #subnav -->
    </div>

    <div class="page">
        <div id="content_n" class="pageContent">
            <div class="clear"></div>
            <div class="content-wide mbot">
                <%--<p class="head1">Current Sales</p>--%>
            </div>
            <div style="clear:both;"></div>

            <div class="module" id="searchDiv">
                <h2>
                    <div class="inner_bg">
                        <span id="searchBox">Search</span>
                        <select class="filter-sel">
                            <option>Select Report Type</option>
                            <option>Report1</option>
                        </select>
                        <div class="filter_btns">
                            <a href="javascript:void(0);" onclick="javascript:return exportTo('json');" class="button1">JSON</a>
                            <a href="javascript:void(0);" onclick="javascript:return exportTo('xml');" class="button1">XML</a>
                            <a href="javascript:void(0);" onclick="javascript:return exportTo('pdf');" class="button1">PDF</a>
                            <a href="javascript:void(0);" onclick="javascript:return exportTo('csv');" class="button1">CSV</a>
                            <a href="javascript:void(0);" onclick="javascript:return exportTo('excel');" class="button1">Excel</a>
                        </div>
                    </div>
                </h2>
                <div id="searchContainer" class="module-body" style="display: no ne;">
                    <div class="fild-div">
                        <label>From Date:</label><input type="text" class="date-pick text4" title="Please select [From] date" name="txtFromDate" id="txtFromDate" maxlength="10" />
                    </div>
                    <div class="fild-div">
                        <label>To Date:</label>
                        <input type="text" class="date-pick text4" title="Please select [From] date" name="txtToDate" id="txtToDate" maxlength="10" />
                    </div>
                    <div class="fild-div">
                        <input type="button" name="Search" value="Search" class="button1" onclick="javascript:searchCurrentSale();" />
                    </div>
                    <div style="clear:both;"></div><br/>
                </div>
            </div>

            <div class="chart-section" id="reportDataArea">
                <div class="col_50" >
                    <div class="sec_header" id="sec_header">Current Sales Report</div>
                    <div class="sec_box">
                        <div id="Div1" class="dvResults">
                            <table class="Data rounded  tblData">
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col_50r" >
                    <div class="sec_header">
                        Graphical View
                        <div class="filter_btns" style="margin-top:-3px;">
                            <a href="javascript:void(0);" onclick="javascript:return setChart('pie');" class="button1">Pie Chart</a>
                            <a href="javascript:void(0);" onclick="javascript:return setChart('bar');" class="button1">Bar Chart</a>
                        </div>
                    </div>
                    <div class="sec_box">
                        <div class="module-body">
                            <img src="../image/chart/1/BarChart.png" alt="Sales" width="400" height="400" id="imgChart" />
                            <div style="clear:both;"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="clear:both;"></div>
        </div>
    </div>
    </div>
    <script language="javascript" type="text/javascript">
        //Add event toggle on #toggle
        AddDatePicker('#txtFromDate'); AddDatePicker('#txtToDate');
        getCurrentSale();
    </script>
</body>
</html>
