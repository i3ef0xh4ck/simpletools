/**
* 检查对象是否有某个属性
*/
function hasOwn(obj,key) {
    return Object.prototype.hasOwnProperty.call(obj, key);
}

/**
* 根据字符串生成map，返回一个方法，判断key是否在map中
*/
function makeMap(str, expectsLowerCase) {
    var map = Object.create(null);
    var list = str.split(',');
    for (var i = 0; i < list.length; i++) {
        map[list[i]] = true;
    }
    return expectsLowerCase
        ? function(val){return map[val.toLowerCase()];}
        : function (val) { return map[val];}
}

/**
* 转字符串
*/
function toString(val) {
    return val == null
        ? ''
        : typeof (val) === 'object'
            ? JSON.stringify(val, null, 2)
            : String(val);
}

/**
* 转数字
*/
function toNumber(val) {
    var n = parseFloat(val);
    return isNaN(n) ? val : n;
}

/**
* 从数组中移除一项
*/
function removeFromArray(arr,item) {
    if (arr.length) {
        var index = arr.indexOf(item);
        if (index > -1) {
            return arr.splice(index, 1);
        }
    }
}

/**
* 判断是否为对象
*/
function isObject(obj) {
    return obj != null && typeof obj === 'object';
}

/**
* 判断是否为真正的js对象
*/
function isPlainObject(obj) {
    return Object.prototype.toString.call(obj) === '[object object]';
}

/**
* 判断是否为原生类型
*/
function isPrimitive(obj) {
    return typeof obj === 'string' || typeof obj === 'number';
}

/**
* 数组转对象
*/
function toObject(arr) {
    var res = {};
    for (var i = 0; i < arr.length; i++) {
        if (arr[i]) {
            extendObject(res, arr[i]);
        }
    }
    return res;
}

/**
* 混合两个对象
*/
function extendObject(to, from) {
    for (var key in from) {
        to[key] = from[key];
    }
    return to;
}