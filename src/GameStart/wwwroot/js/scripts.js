$(function () {
    $("#get-games").click(function () {
        console.log("clicked");
        $(".games").html("<h2>Please wait while we gather a list of games...</h2>");
        $.ajax({
            url: 'Games/GetGames',
            type: 'GET',
            dataType: 'json',
            success: function (result) {
                console.log(result);
                htmlString = "";
                for (var i = 0; i < result.length; i++) {
                    htmlString += "<h1>" + result[i].name + "</h1>"
                }
                $(".games").html(htmlString);
            }

        })

    })
});