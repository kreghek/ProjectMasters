function getContainer(){

    
    let body = new PIXI.Sprite(PIXI.loader.resources["images/empty_body.png"].texture);
    let shirt = new PIXI.Sprite(PIXI.loader.resources["images/shirt.png"].texture);
    let eyes = new PIXI.Sprite(PIXI.loader.resources["images/eyes.png"].texture);

    let container = new PIXI.Container();
    container.addChild(body);
    container.addChild(shirt);
    container.addChild(eyes);
    container.scale = new PIXI.Point(0.1, 0.1);
    

    return container;

}