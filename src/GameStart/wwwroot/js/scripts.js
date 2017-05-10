$(function () {

    $("#get-games").click(function () {
        console.log("clicked");
        $(".games").html("<h2>Please wait while we gather a list of games...</h2>");
        $.ajax({
            url: 'Games/GetGames',
            type: 'GET',
            dataType: 'json',
            data: { searchQuery: $("#searchQuery").val() },
            success: function (result) {
                console.log(result);
                var htmlString = "<div class='cols'>";
                for (var i = 0; i < result.length; i++) {
                    htmlString += "<div class='game-box gameObject' id='" + result[i].id + "'><h4>" + result[i].name + "</h4></div>"
                }
                htmlString += "</div>";
                // Output list of games found to div
                $(".games").html(htmlString);
                // Start of AJAX for game detail
                $(".gameObject").click(function () {
                    //Grab individual game
                    var gameId = this.id;
                    $.ajax({
                        url: 'Games/Details',
                        type: 'GET',
                        dataType: 'json',
                        data: { id: gameId },
                        success: function (result) {
                            console.log(result);
                            if (result[0].rating && result[0].screenshots)
                            {
                                var htmlString = "<div class='game-detail'>" +
                                    "<img class='gameImg' src='" + result[0].screenshots[0].url.replace("t_thumb/", "") + "' />" +
                                    "<h2> Game Title: " + result[0].name + "</h2>" +
                                    "<h3> Rating: " + result[0].rating.toFixed(2) + "</h3>" +
                                    "<h4> Summary: " + result[0].summary + "</h4>" +
                                    "<button type='button' class='claim' id='" + result[0].id + "'>Add to your collection</button>" +
                                    "</div>";
                            }
                            else {
                                var htmlString = "<div class='game-detail'>" +
                                    "<h2> Game Title: " + result[0].name + "</h2>" +
                                    "<h3> Rating: No Rating </h3>" +
                                    "<h4> Summary: " + result[0].summary + "</h4>" +
                                    "<button type='button' class='claim' id='" + result[0].id + "'>Add to your collection</button>" +
    "</div>";
                            }
                            // Output Game Detail to div
                            $(".games").html(htmlString);
                            // Start of AJAX for adding game to users collection
                            $(".claim").click(function () {
                                console.log("you have claimed it " + this.id);
                                $.ajax({
                                    url: 'Games/ClaimGame',
                                    type: 'POST',
                                    dataType: 'json',
                                    data: { id: this.id },
                                    success: function (result) {
                                        console.log(result);
                                    }
                                })
                            })
                        }
                    });
                })
            }
        })
    });
});