import{h,u as V,E as y,c as i,a as k,o as l,b as c,d as t,t as v,m as _,F as b,l as g,z as w,e as K}from"./vendor.a781799b.js";import{_ as x,v as d,A as o,P as n,l as p,a as C}from"./index.3abba102.js";const H={setup(){const u=h(),s=V();return y(async()=>{try{await d.activeVault(u.params.vaultId),await d.getKeepFromVault(o.activeVault.id),o.activeVault.name==null&&(s.push({name:"Home"}),n.toast("You can't view private vaults"))}catch(a){p.error(a),n.toast("This Vault is private"),s.push({name:"Home"})}}),{async deleteVault(){try{await n.confirm("Do you want to delete this vault")&&(await d.deleteVault(o.activeVault.id),n.toast("Vault deleted, sending you Home","success"),s.push({name:"Home"}))}catch(a){p.error(a),n.toast(a.message,"error")}},async removeKeep(a,e){await n.confirm("Are you sure you want to remove this keep?")&&await C.removeKeep(a,e)},account:i(()=>o.account),vault:i(()=>o.activeVault),keeps:i(()=>o.keeps),profile:i(()=>o.profile)}}},I={class:"main-container"},N={class:"row m-4"},P={class:"col-12 d-flex justify-content-between"},S={class:"text-dark"},j={class:"col-12 mt-3 text-dark"},A=w("Keeps: "),B={class:"masonry-frame ms-4"},D={key:0,class:"col-12 delete-button"},F=["onClick"];function R(u,s,a,e,E,T){var m;const f=k("Keep");return l(),c("div",I,[t("div",N,[t("div",P,[t("h1",S,v(e.vault.name),1),((m=e.vault.creator)==null?void 0:m.id)==e.account.id?(l(),c("button",{key:0,class:"btn btn-outline-dark",onClick:s[0]||(s[0]=(...r)=>e.deleteVault&&e.deleteVault(...r))}," Delete Vault ")):_("",!0)]),t("div",j,[t("h5",null,[A,t("span",null,v(e.keeps.length),1)])])]),t("div",B,[(l(!0),c(b,null,g(e.keeps,r=>(l(),c("div",{key:r.id,class:"",title:"Go to Keep"},[K(f,{keep:r},null,8,["keep"]),e.vault.creatorId==e.account.id?(l(),c("div",D,[t("i",{class:"btn mdi mdi-delete mt-1",title:"Remove keep from vault",onClick:z=>e.removeKeep(r.vaultKeepId,r.id)},null,8,F)])):_("",!0)]))),128))])])}var M=x(H,[["render",R],["__scopeId","data-v-4f1b21c6"]]);export{M as default};