function getPersonContainer(eyesIndex, hairIndex, mouthIndex, textures) {
    let body = new PIXI.Sprite(textures.empty_body.texture);
    let shirt = new PIXI.Sprite(textures.shirt.texture);

    let eyes1 = new PIXI.Sprite(textures.eyes1.texture);
    let eyes2 = new PIXI.Sprite(textures.eyes2.texture);
    let eyes3 = new PIXI.Sprite(textures.eyes3.texture);
    let eyes = [eyes1, eyes2, eyes3];

    let hair1 = new PIXI.Sprite(textures.hair1.texture);
    let hair2 = new PIXI.Sprite(textures.hair2.texture);
    let hair3 = new PIXI.Sprite(textures.hair3.texture);
    let hairs = [hair1, hair2, hair3];


    let mouth1 = new PIXI.Sprite(textures.mouth1.texture);
    let mouth2 = new PIXI.Sprite(textures.mouth2.texture);
    let mouth3 = new PIXI.Sprite(textures.mouth3.texture);
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

function getUnitContainer(type, textures) {

    let body = getContainerByType(type, textures);

    body.pivot = new PIXI.Point(0.5, 1);

    let container = new PIXI.Container();
    container.addChild(body);
    container.scale = new PIXI.Point(0.12, 0.12);
    container.pivot = new PIXI.Point(0.5, 1);

    return container;
}

function getContainerByType(type, textures) {
    if (type == "Feature") {
        return  new PIXI.Sprite(textures.feature.texture);
    }

    if (type == "Error") {
        return new PIXI.Sprite(textures.bug.texture);
    }

    return new PIXI.Sprite(textures.bug.texture);
}