<template>
    <div data-bs-target="#keep-modal" data-bs-toggle="modal" class="m-4 selectable rounded" @click="setActiveProfile">
        <div class="container main-container">


            <img class="img-fluid masonry-pics" :src="keep.img" />

            <div class=" row  text-light keep-text">

                <div class="col-12 d-flex justify-content-between">

                    <h3 class="p-3 h4 name">{{ keep.name }}</h3>
                    <img class=" profile-pic p-2 selectable" @click.stop="takeToProfile" :src="keep.creator?.picture" />
                </div>

                <!-- <div class="col-2 d-flex justify-content-end">
                </div> -->
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
.main-container {
    position: relative;
    text-align: left;
    color: white;

}

.bottom-left {
    position: absolute;
    bottom: -10px;
    left: 16px;
}

.profile-pic {

    width: 50px;
    height: 50px;
    border-radius: 50%;
    // margin-left: auto;

}

.name {
    text-shadow: 2px 2px black;
}

.keep-text {
    position: relative;
    bottom: 65px;
}

.masonry-pics {
    border-radius: 10%;
}
</style>