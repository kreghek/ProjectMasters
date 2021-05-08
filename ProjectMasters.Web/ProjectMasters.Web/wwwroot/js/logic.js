function getPersonContainer(){

    
    let body = new PIXI.Sprite(PIXI.loader.resources["images/empty_body.png"].texture);
    let shirt = new PIXI.Sprite(PIXI.loader.resources["images/shirt.png"].texture);
    let eyes = new PIXI.Sprite(PIXI.loader.resources["images/eyes.png"].texture);

    let container = new PIXI.Container();
    container.addChild(body);
    container.addChild(shirt);
    container.addChild(eyes);
    container.scale = new PIXI.Point(0.1, 0.1);
    container.pivot = new PIXI.Point(0.5, 1);

    return container;

}

function getUnitContainer() {


    let body = new PIXI.Sprite(PIXI.loader.resources["images/unit.png"].texture);
    body.pivot = new PIXI.Point(0.5, 1);

    let container = new PIXI.Container();
    container.addChild(body);
    container.scale = new PIXI.Point(0.5, 0.5);
    container.pivot = new PIXI.Point(0.5, 1);


    return container;

}