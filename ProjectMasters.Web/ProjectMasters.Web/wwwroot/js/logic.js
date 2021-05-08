function getPersonContainer(eyesIndex, hairIndex, mouthIndex, textures) {
    let body = new PIXI.Sprite(textures.empty_body);
    let shirt = new PIXI.Sprite(textures.shirt);

    let eyes1 = new PIXI.Sprite(textures.eyes1);
    let eyes2 = new PIXI.Sprite(textures.eyes2);
    let eyes3 = new PIXI.Sprite(textures.eyes3);
    let eyes = [eyes1, eyes2, eyes3];

    let hair1 = new PIXI.Sprite(textures.hair1);
    let hair2 = new PIXI.Sprite(textures.hair2);
    let hair3 = new PIXI.Sprite(textures.hair3);
    let hairs = [hair1, hair2, hair3];


    let mouth1 = new PIXI.Sprite(textures.mouth1);
    let mouth2 = new PIXI.Sprite(textures.mouth2);
    let mouth3 = new PIXI.Sprite(textures.mouth3);
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

function getUnitContainer(type, textures, masteryItems) {

    let sprite = getSpriteByType(type, textures, masteryItems);

    sprite.pivot = new PIXI.Point(0.5, 1);

    let container = new PIXI.Container();
    container.addChild(sprite);
    container.scale = new PIXI.Point(0.12, 0.12);
    container.pivot = new PIXI.Point(0.5, 1);

    return container;
}

function getSpriteByType(type, textures, masteryItems) {
    if (type == "Feature") {
        return new PIXI.Sprite(textures.feature);
    }

    if (type == "Error") {
        return new PIXI.Sprite(textures.bug2);
    }
    if (type == "SubTask") {
        if (masteryItems.includes("backend") && !masteryItems.includes("frontend")) {
            return new PIXI.Sprite(textures.backendTask);
        }
        if (masteryItems.includes("frontend") && !masteryItems.includes("backend")) {
            return new PIXI.Sprite(textures.frontendTask);
        }
        if (masteryItems.includes("frontend") && masteryItems.includes("backend")) {
            return new PIXI.Sprite(textures.frontendTask);//TODO подставить fullstack task
        }
    }


    return new PIXI.Sprite(textures.bug1);
}