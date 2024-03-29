﻿$(function () {
    Set_Data();
});
function Set_Data() {
    var old_generateMonthYearHeader = $.datepicker._generateMonthYearHeader;
    var old_get = $.datepicker._get;
    var old_CloseFn = $.datepicker._updateDatepicker;
    $.extend($.datepicker, {
        _generateMonthYearHeader: function (a, b, c, d, e, f, g, h) {
            var htmlYearMonth = old_generateMonthYearHeader.apply(this, [a, b, c, d, e, f, g, h]);
            if ($(htmlYearMonth).find(".ui-datepicker-year").length > 0) {
                htmlYearMonth = $(htmlYearMonth).find(".ui-datepicker-year").find("option").each(function (i, e) {
                    //if (Number(e.value) - 1911 > 0) $(e).text(Number(e.innerText) -1911);
                    if (Number(e.value) - 1911 > 0) $(e).text(Number(e.value) - 1911);
                }).end().end().get(0).outerHTML;
            }
            return htmlYearMonth;
        },
        _get: function (a, b) {
            a.selectedYear = a.selectedYear - 1911 < 0 ? a.selectedYear + 1911 : a.selectedYear;
            a.drawYear = a.drawYear - 1911 < 0 ? a.drawYear + 1911 : a.drawYear;
            a.curreatYear = a.curreatYear - 1911 < 0 ? a.curreatYear + 1911 : a.curreatYear;
            return old_get.apply(this, [a, b]);
        },
        _updateDatepicker: function (inst) {
            old_CloseFn.call(this, inst);
            $(this).datepicker("widget").find(".ui-datepicker-buttonpane").children(":last")
                   .click(function (e) {
                       inst.input.val("");
                   });
        },
        _setDateDatepicker: function (a, b) {
            if (a = this._getInst(a)) { this._setDate(a, b); this._updateDatepicker(a); this._updateAlternate(a) }
        },
        _widgetDatepicker: function () {
            return this.dpDiv
        }

    });

    $(".date").datepicker({
        yearSuffix: "", //將年改為空白
        changeYear: true, //手動修改年
        changeMonth: true, //手動修改月
        showWeek: true, //顯示第幾周
        firstDay: 1, //0為星期天
        showOtherMonths: true, //在本月中顯示其他月份
        selectOtherMonths: true, //可以在本月中選擇其他月份
        showButtonPanel: true, //顯示bottom bar
        closeText: '清除', //將離開改為清除
        //appendText: "yyy-mm-dd",
        dateFormat: "yy-mm-dd",
        onSelect: function (dateText, inst) {
            var dateFormate = inst.settings.dateFormat == null ? "yy/mm/dd" : inst.settings.dateFormat; //取出格式文字
            var reM = /m+/g;
            var reD = /d+/g;
            var objDate = {
                y: inst.selectedYear - 1911 < 0 ? inst.selectedYear : inst.selectedYear - 1911,
                m: String(inst.selectedMonth).length != 1 ? inst.selectedMonth + 1 : "0" + String(inst.selectedMonth + 1),
                d: String(inst.selectedDay).length != 1 ? inst.selectedDay : "0" + String(inst.selectedDay)
            };
            $.each(objDate, function (k, v) {
                var re = new RegExp(k + "+");
                dateFormate = dateFormate.replace(re, v);
            });
            inst.input.val(dateFormate);
        }
    });
}