$(document).ready(() => {

  let width = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
  let height = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;
  let image;

  const game = new Phaser.Game(
    width,
    height,
    Phaser.CANVAS,
    'phaser-example',
    {
        preload: preload,
        create: create,
        update: update,
        render : render
    }
  );

  function preload(){
      image = game.load.image('backdrop', 'lib/map.svg');

  };

  function create(){
      game.world.setBounds(-100,-65,width,height);
      let sprite = game.add.sprite(game.world.centerX-image.width/2,game.world.centerY-image.height/2,'backdrop');
      game.stage.backgroundColor = '#1D375C';
      sprite.scale.setTo(0.55, 0.55);
      sprite.inputEnabled = true;
      sprite.events.onInputDown.add(clickOnWorld, this);
  };

  function update(){
    console.log("Update");
    width = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
    height = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;
    game.scale.setGameSize(width, height);
  };

  function render(){
      // this.game.debug.cameraInfo(this.game.camera, 500, 32);
  };

  const clickOnWorld = () => {

      console.log("Map clicked");

  };

});
