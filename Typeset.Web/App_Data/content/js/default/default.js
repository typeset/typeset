﻿/// <reference path="../base/jquery-1.7.2-vsdoc.js" />
$(document).ready(function () {
    //Apply twitter Bootstrap classes
    $('body').addClass('container');
    $('body > header').addClass('span2');
    $('body > footer').addClass('row-fluid span12');
    $('#content').addClass('row-fluid');
    $('#content > .posts').addClass('span8');
    $('#content > article.post').addClass('span8');
});