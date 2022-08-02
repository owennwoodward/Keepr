<template>
  <div class="container-fluid">

    <div class="masonry-frame">
      <div v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService.js'
import { profilesService } from '../services/ProfilesService.js'

export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAll()
        // await profilesService.getProfile()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps),

      // profile: computed(() => AppState.profiles)
    }
  }
}
</script>


<style lang="scss" scoped>
body {
  margin: 0;
  padding: 1rem;
}

.masonry-frame {
  columns: 6 200px;
  column-gap: 1rem;

  div {
    width: 150px;
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
  }

  @for $i from 1 through 36 {
    div:nth-child(#{$i}) {
      $h: (random(400) + 100)+px;
      height: $h;
      line-height: $h;
    }
  }
}
</style>