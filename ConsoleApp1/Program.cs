using Game.Models;

var player = new Player();
var mineField = MineFieldGame.GenerateMineField();
var game = new MineFieldGame(player, mineField);
game.Start();