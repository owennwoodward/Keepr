<template>
    <div data-bs-target="#keep-modal" data-bs-toggle="modal" class="m-4 elevation-2 justify-content-between selectable"
        @click="setActiveProfile">
        <div class="p-3">
            <div class="over-container " :style="{ 'background-image': `url(${keep.img})` }">
                <div class="d-flex justify-content-between ">
                    <h3 class="p-3 text-light">{{ keep.name }}</h3> <img class=" profile-pic p-2 selectable"
                        @click.stop="takeToProfile" :src="keep.creator?.picture" />
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { Modal } from 'bootstrap'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'

export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    setup(props) {
        const router = useRouter()
        return {
            takeToProfile() {
                router.push({
                    name: 'Profile',
                    params: {
                        id: props.keep.creatorId
                    }
                })
            },

            async setActiveProfile() {
                try {
                    await keepsService.setActive(props.keep.id)
                    // AppState.activeKeep.views++
                    // await Modal.getOrCreateInstance(document.getElementById('keep-modal')).show()
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.over-container {
    position: relative;
    height: 20vw;
    background-repeat: no-repeat;
    background-size: cover;
    // border-radius: 5%;

}

.over-top {
    z-index: 50;
    background-color: rgba(223, 200, 200, 0.347);
    backdrop-filter: blur(5px);
    color: white;
    text-shadow: 1px 1px #252222;
    // border-radius: 5%;

}

.profile-pic {

    width: 70px;
    height: 70px;
    border-radius: 50%;
    margin-left: 10px;
}
</style>