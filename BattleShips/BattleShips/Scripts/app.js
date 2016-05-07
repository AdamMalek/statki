/// <reference path="angular.js" />

var app = angular.module("battleships", [])
                 .controller("gameController", ["$http", "$compile", "$scope", function ($http, $compile, $scope) {
                     var vm = this;

                     function DrawBoard(board, id, playerid) {
                         $("#player" + id).removeClass("player-move");
                         $("#player" + id + "board").html("");
                         var html = "";
                         for (var i = 0; i < board.length; i++) {
                             html += "<tr>";
                             for (var j = 0; j < board[i].length; j++) {
                                 switch (board[j][i]) {
                                     case 1:
                                         fieldclass = "hit";
                                         break;
                                     case 2:
                                         fieldclass = "miss";
                                         break;
                                     case 0:
                                         fieldclass = "covered";
                                         break;
                                 }
                                 field = "<td class=" + fieldclass + " ng-click=\"game.shot(" + playerid + "," + j + "," + i + ")\"></td>";
                                 html += field;
                             }
                             html += "</tr>";
                         }
                         var temp = $compile(html)($scope);
                         angular.element(document.getElementById("player" + id + "board")).append(temp);
                         if (playerid == vm.playerTurnId)
                             $("#player" + id).addClass("player-move");
                     };

                     function ReloadGame(response) {
                         vm.status = response.Status;
                         vm.end = response.end;
                         vm.playerTurn = response.PlayerTurn;
                         vm.playerTurnId = response.PlayerTurnId;
                         vm.player1 = response.Players[0].PlayerName;
                         vm.player1id = response.Players[0].PlayerId;
                         vm.player2 = response.Players[1].PlayerName;
                         vm.player2id = response.Players[1].PlayerId;
                         DrawBoard(response.Board1, 1, vm.player1id);
                         DrawBoard(response.Board2, 2, vm.player2id);
                     };

                     var id = $("#gameId").text();
                     vm.gameId = id;

                     $http.get("/api/game/" + id).success(function (response) {
                         ReloadGame(response);
                     });

                     vm.shot = function (playerId, x, y) {
                         var data = {
                             GameId: vm.gameId,
                             PlayerId: vm.playerTurnId,
                             Shot: {
                                 X: x,
                                 Y: y
                             }
                         };

                         if (playerId != vm.playerTurnId || vm.end) {
                             vm.status = "This player cannot shoot now!";
                             return;
                         }

                         $http.post("/api/game/", data).success(function (response) {
                             ReloadGame(response);
                         });
                     }
                 }]);