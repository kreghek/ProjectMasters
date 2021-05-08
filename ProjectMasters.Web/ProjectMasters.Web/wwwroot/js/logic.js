function getPersonContainer(eyesIndex, hairIndex, mouthIndex) {
    let body = new PIXI.Sprite(PIXI.loader.resources["images/empty_body.png"].texture);
    let shirt = new PIXI.Sprite(PIXI.loader.resources["images/shirt.png"].texture);

    let eyes1 = new PIXI.Sprite(PIXI.loader.resources["images/eyes.png"].texture);
    let eyes2 = new PIXI.Sprite(PIXI.loader.resources["images/eyes2.png"].texture);
    let eyes3 = new PIXI.Sprite(PIXI.loader.resources["images/eyes3.png"].texture);
    let eyes = [eyes1, eyes2, eyes3];

    let hair1 = new PIXI.Sprite(PIXI.loader.resources["images/hair1.png"].texture);
    let hair2 = new PIXI.Sprite(PIXI.loader.resources["images/hair2.png"].texture);
    let hair3 = new PIXI.Sprite(PIXI.loader.resources["images/hair3.png"].texture);
    let hairs = [hair1, hair2, hair3];


    let mouth1 = new PIXI.Sprite(PIXI.loader.resources["images/mouth1.png"].texture);
    let mouth2 = new PIXI.Sprite(PIXI.loader.resources["images/mouth2.png"].texture);
    let mouth3 = new PIXI.Sprite(PIXI.loader.resources["images/mouth3.png"].texture);
    let mouth = [mouth1, mouth2, mouth3];


    let container = new PIXI.Container();
    container.addChild(body);
    container.addChild(shirt);

    container.addChild(eyes[eyesIndex - 1]);
    container.addChild(hairs[hairIndex - 1]);
    container.addChild(mouth[mouthIndex - 1]);

    container.scale = new PIXI.Point(0.1, 0.1);
    container.pivot = new PIXI.Point(0.5, 1);

    return container;
}

function getUnitContainer(type) {
    let body = new PIXI.Sprite(PIXI.loader.resources["images/bug.png"].texture);
    body.pivot = new PIXI.Point(0.5, 1);

    if (type == "Feature") {
        body.tint = 16776960;
    }

    if (type == "Error") {
        body.tint = 16711680;
    }

    let container = new PIXI.Container();
    container.addChild(body);
    container.scale = new PIXI.Point(0.12, 0.12);
    container.pivot = new PIXI.Point(0.5, 1);

    return container;

}