//Create a self executing function
;(function(w, d){
    var _viewElement = null,
        _defaultRoute = null,
        _rendered = false;

        var jsMVC = function() {
            this._routeMap = {};
        }

        jsMVC.prototype.AddRoute = function(controller, route, template) {
            this._routeMap[route] = new routeObj(controller, route, template);
        }

        jsMVC.prototype.Initialize = function() {
            //create the update view delegate
            //get the view element reference
            //set a defualt route
            //wire up the hash change even with the update view delegate
            //call the update view delegate
        }
        var routeObj = function (c, r, t) {
            this.controller = c,
            this.route = r,
            this.template = t;
        }
})(window, document);

//Here are the steps
//Add the route information
//create an update view function
//wire up the updated view function with hash changes
//fetch the view html
//call the controller function
//replace the tokens
//render the view html in the view container