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
            var updateViewDelegate = updateView.bind(this);

            //get the view element reference
            _viewElement = d.querySelector("[view]");
            if(!_viewElement) return;

            //set a defualt route
            _defaultRoute = this._routeMap[Object.getOwnPropertyNames(this._routeMap)[0]];

            //wire up the hash change even with the update view delegate
            w.onhashchange = updateViewDelegate;

            //call the update view delegate
            updateViewDelegate();
        }

        function updateView () {
            //get the route name from the address bar hash
            var pageHash = w.location.hash.replace('#', ''),
            routeName = null,
            routeObj = null;

            routeName = pageHash.replace('/', '');
            _rendered = false;

            //fetch the route object using the route name
            routeObj = this._routeMap[routeName];
            
            //route name is not found then use default route
            if(!routeObj){
                routeObj = _defaultRoute;
            }
            //render the view html associated with the route
            loadTemplate(routeObj, _viewElement)
        }

        function loadTemplate(routeObject, viewElement){
            var xmlhttp;
            if(window.XMLHttpRequest){
                xmlhttp = new XMLHttpRequest();
            }else{
                xmlhttp = new ActiveXObject('Microsoft.XMLHTTP');
            }
            
            xmlhttp.onreadystatechange = function() {
                if(xmlhttp.readyState == 4 && xmlhttp.status == 200){
                    //load the view
                    loadView(routeObject, viewElement, xmlhttp.responseText);
                }
            }
            xmlhttp.open('GET', routeObject.template, true);
            xmlhttp.send();
        }

        function loadView(routeOject, viewElement, viewHTML) {
            //create model object
            var model = {};

            //call controller function of the route
            routeOject.controller(model);

            //replace the view html tokens with the model properties
            viewHTML = replaceTokens(viewHTML, model);

            //render the view.
            if(!_rendered) {
                viewElement.innerHTML = viewHTML;
                _rendered = true
            }
        }

        function replaceTokens(viewHTML, model){
            var modelProps = Object.getOwnPropertyNames(model);

            modelProps.forEach(function(element, index, array) {
                viewHTML = viewHTML.replace('{{' + element + '}}', model[element]);
            });

            return viewHTML;
        }
        var routeObj = function (c, r, t) {
            this.controller = c,
            this.route = r,
            this.template = t;
        }

        w['jsMVC'] = new jsMVC();
})(window, document);

//Here are the steps
//Add the route information
//create an update view function
//wire up the updated view function with hash changes
//fetch the view html
//call the controller function
//replace the tokens
//render the view html in the view container