import{_ as n,k as c,l as p,P as _,A as d}from"./index.3abba102.js";import{h as i,E as l,c as m,a as u,o as a,b as s,d as v,F as f,l as k,e as g}from"./vendor.a781799b.js";const h={name:"Home",setup(){return i(),l(async()=>{try{await c.getAll()}catch(e){p.error(e),_.toast(e.message,"error")}}),{keeps:m(()=>d.keeps)}}},y={class:"container-fluid"},x={class:"masonry-frame p-3"};function b(e,P,A,o,B,H){const r=u("Keep");return a(),s("div",y,[v("div",x,[(a(!0),s(f,null,k(o.keeps,t=>(a(),s("div",{key:t.id},[g(r,{keep:t,title:"Go to this Keep"},null,8,["keep"])]))),128))])])}var j=n(h,[["render",b],["__scopeId","data-v-61ec3af8"]]);export{j as default};