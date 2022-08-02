<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <KeepModal />
    <VaultForm />
    <KeepForm />
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
.profile-pic {

  width: 70px;
  height: 70px;
  border-radius: 50%;
  margin-left: 10px;
}


@import "./assets/scss/main.scss";
</style>
