var myLayout = {

    //-----------Windows
    getWindowsInnerHeight: function () {
        return window.innerHeight;
    },
    getWindowsInnerWidth: function () {
        return window.innerWidth;
    },
    getWindowsOuterHeight: function () {
        return window.outerHeight;
    },
    getWindowsOuterWidth: function () {
        return window.outerWidth;
    },

    //-----------Document
    getDoc_clientWidth: function () {
        return document.documentElement.clientWidth;
    },
    getDoc_clientHeight: function () {
        return document.documentElement.clientHeight;
    },
    getDoc_clientLeft: function () {
        return document.documentElement.clientLeft;
    },
    getDoc_clientTop: function () {
        return document.documentElement.clientTop;
    },
    getDoc_offsetWidth: function () {
        return document.documentElement.offsetWidth;
    },
    getDoc_offsetHeight: function () {
        return document.documentElement.offsetHeight;
    },
    getDoc_offsetLeft: function () {
        return document.documentElement.offsetLeft;
    },
    getDoc_offsetTop: function () {
        return document.documentElement.offsetTop;
    },
    getDoc_scrollHeight: function () {
        //read-only property is a measurement of the height of an element's content, including content not visible on the screen due to overflow.
        //scrollHeight value is equal to the minimum height the element would require in order to fit all the content in the viewport without using a vertical scrollbar.
        return document.documentElement.scrollHeight;
    },
    getDoc_SetHeight: function (heightVal) {
        return document.documentElement.style.height = heightVal + "px";
    },
    //-----------Body
    getBody_clientWidth: function () {
        return document.body.clientWidth;
    },
    getBody_clientHeight: function () {
        return document.body.clientHeight;
    },
    //-----------Elements
    getElmntRect: function getRect(elm) {
        return elm.getBoundingClientRect();
    },

    getElm_Top: function (elm) {
        
        return this.getElmntRect(elm).top;
    },
    
    getElm_Bottom: function (elm) {
        return this.getElmntRect(elm).bottom;
    },

    getElm_Left: function (elm) {
        return this.getElmntRect(elm).left;
    },

    getElm_Right: function (elm) {
        return this.getElmntRect(elm).right;
    },
    getElm_height: function (elm) {
        return this.getElmntRect(elm).height;
    },
    getElm_Width: function (elm) {
        return this.getElmntRect(elm).width;
    },

    getElm_X: function (elm) {
        return this.getElmntRect(elm).x;
    },

    getElm_Y: function (elm) {
        return this.getElmntRect(elm).y;
    },
    getElm_offsetTop: function (elm) {
        return elm.offsetTop;
    },
    getElm_offsetHeight: function (elm) {
        return elm.offsetHeight;
    },
    getElm_offsetLeft: function (elm) {
        return elm.offsetLeft;
    },
    getElm_offsetParent: function (elm) {
        return elm.offsetParent;
    },
    getElm_offsetWidth: function (elm) {
       
        return elm.offsetWidth;
    },
    setElm_Height: function (elm, height) {
        elm.style.height = height + "px";
        return elm
    },
    setElm_Height_Percent: function (elm, hPercent) {
        elm.style.height = hPercent + "%";
        return elm
    },
    setElm_width: function (elm, width) {
        elm.style.width = width + "px";
        return elm
    },
    setElm_width_Percent: function (elm, wPercent) {
        elm.style.width = wPercent + "%";
        return elm
    },
    setElm_Top: function (elm, top) {
        elm.style.top = top + "px";
        return elm
    },
    isElementInViewport: function (el, fullyView){
        var top = el.offsetTop;
        var left = el.offsetLeft;
        var width = el.offsetWidth;
        var height = el.offsetHeight;

        while (el.offsetParent) {
            el = el.offsetParent;
            top += el.offsetTop;
            left += el.offsetLeft;
        }

        if (fullyView) {
            return (
                        top >= window.pageYOffset &&
                        left >= window.pageXOffset &&
                        (top + height) <= (window.pageYOffset + window.innerHeight) &&
                        (left + width) <= (window.pageXOffset + window.innerWidth)
                      );
        }
        else {
            return (
                         top < (window.pageYOffset + window.innerHeight) &&
                         left < (window.pageXOffset + window.innerWidth) &&
                         (top + height) > window.pageYOffset &&
                         (left + width) > window.pageXOffset
                       );
        }

    },

}