function setBackground()
{
      var width = window.innerWidth;
      var height = window.innerHeight;
      function update(activeAnchor)
      {
            var group = activeAnchor.getParent();

            var topLeft = group.get('.topLeft')[0];
            var topRight = group.get('.topRight')[0];
            var bottomRight = group.get('.bottomRight')[0];
            var bottomLeft = group.get('.bottomLeft')[0];
            var image = group.get('Image')[0];

            var anchorX = activeAnchor.getX();
            var anchorY = activeAnchor.getY();

      // update anchor positions
            switch (activeAnchor.getName())
            {
                  case 'topLeft':
                  topRight.y(anchorY);
                  bottomLeft.x(anchorX);
                  break;
                  case 'topRight':
                  topLeft.y(anchorY);
                  bottomRight.x(anchorX);
                  break;
                  case 'bottomRight':
                  bottomLeft.y(anchorY);
                  topRight.x(anchorX);
                  break;
                  case 'bottomLeft':
                  bottomRight.y(anchorY);
                  topLeft.x(anchorX);
                  break;
            }

            image.position(topLeft.position());
            var width = topRight.getX() - topLeft.getX();
            var height = bottomLeft.getY() - topLeft.getY();
            if (width && height)
            {
                  image.width(width);
                  image.height(height);


            }
      }
      function addAnchor(group, x, y, name)
      {
            var stage = group.getStage();
            var layer = group.getLayer();

            var anchor = new Konva.Circle(
            {
                  x: x,
                  y: y,
                  stroke: '#666',
                  fill: '#ddd',
                  strokeWidth: 2,
                  radius: 8,
                  name: name,
                  draggable: true,
                  dragOnTop: false
            });

            anchor.on('dragmove', function()
            {
                  update(this);
                  layer.draw();
            });
            anchor.on('mousedown touchstart', function()
            {
                  group.draggable(false);
                  this.moveToTop();
            });
            anchor.on('dragend', function()
            {
                  group.draggable(true);
                  layer.draw();
            });
      // add hover styling
            anchor.on('mouseover', function()
            {
                  var layer = this.getLayer();
                  document.body.style.cursor = 'pointer';
                  this.strokeWidth(4);
                  layer.draw();
            });
            anchor.on('mouseout', function()
            {
                  var layer = this.getLayer();
                  document.body.style.cursor = 'default';
                  this.strokeWidth(2);
                  layer.draw();
            });

            group.add(anchor);
      }

      var stage = new Konva.Stage(
      {
            container: 'backgroundImageContainer',
            width: width,
            height: height
      });

      var layer = new Konva.Layer();
      stage.add(layer);

      var backgroundImage = new Konva.Image(
      {
            width: 200,
            height: 137
      });


      var backgroundImageGroup = new Konva.Group(
      {
            x: 180,
            y: 50,
            draggable: true
      });
      layer.add(backgroundImageGroup);
      backgroundImageGroup.add(backgroundImage);
      addAnchor(backgroundImageGroup, 0, 0, 'topLeft');
      addAnchor(backgroundImageGroup, 200, 0, 'topRight');
      addAnchor(backgroundImageGroup, 200, 138, 'bottomRight');
      addAnchor(backgroundImageGroup, 0, 138, 'bottomLeft');





      var imageObj1 = new Image();
      imageObj1.onload =
      function loadimage() {
            backgroundImage.image(imageObj1);
            layer.draw();
      };


      var imageName=document.getElementById("imageName").value;
      var tangRef = storageRef.child('backgroundImages/'+imageName);

          // First we sign in the user anonymously
      firebase.auth().signInAnonymously().then(function()
       {
          // Once the sign in completed, we get the download URL of the image
          tangRef.getDownloadURL().then(function(url)
          {
          // Once we have the download URL, we set it to our img element
                imageObj1.src = url

          }).catch(function(error)
            {
          // If anything goes wrong while getting the download URL, log the error
            console.error(error);
            });
      });















}
