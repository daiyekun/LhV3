"use strict";
exports.__esModule = true;
exports.LIGHTER_LABEL_COLOR = exports.LIGHT_LABEL_COLOR = exports.DARK_LABEL_COLOR = exports.DARK_MODE_THRESHOLD = exports.devicePixelRatio = exports.debugMode = void 0;
var dpr = 1;
if (typeof window !== 'undefined') {
    dpr = Math.max(window.devicePixelRatio
        || (window.screen && window.screen.deviceXDPI / window.screen.logicalXDPI)
        || 1, 1);
}
exports.debugMode = 0;
exports.devicePixelRatio = dpr;
exports.DARK_MODE_THRESHOLD = 0.4;
exports.DARK_LABEL_COLOR = '#333';
exports.LIGHT_LABEL_COLOR = '#ccc';
exports.LIGHTER_LABEL_COLOR = '#eee';
