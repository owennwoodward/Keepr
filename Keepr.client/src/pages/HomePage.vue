<template>
  <div class="container-fluid ">

    <div class="masonry-frame p-3">
      <div v-for="k in keeps" :key="k.id">
        <Keep :keep="k" title="Go to this Keep" />
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
import { useRoute } from 'vue-router'

export default {
  name: 'Home',
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        // await profilesService.getMyProfile(AppState.profile.id);
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
.masonry-frame {
  columns: 4 200px;
  column-gap: 1rem;

  div {
    break-inside: avoid;
  }
}
</style>